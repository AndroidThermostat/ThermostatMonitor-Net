using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class cp_Charts_Delta : System.Web.UI.Page
{
    public string Data = "";
    DateTime startDate = new DateTime(2000, 1, 1);
    DateTime endDate = DateTime.Now;
    DateTime prevStartDate = new DateTime(2000, 1, 1);
    DateTime prevEndDate = DateTime.Now;
    int timezoneDifference = 0;
    bool includeHistorical = false;
    int thermostatId=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        thermostatId = Convert.ToInt32(Request["ThermostatId"]);
        timezoneDifference = Convert.ToInt32(Request["TimezoneDifference"]);

        if (Request["startDate"] != null) startDate = Convert.ToDateTime(Request["startDate"]).AddHours(timezoneDifference);
        if (Request["endDate"] != null) endDate = Convert.ToDateTime(Request["endDate"]).AddDays(1).AddSeconds(-1).AddHours(timezoneDifference);
        if (Request["prevStartDate"] != null) prevStartDate = Convert.ToDateTime(Request["prevStartDate"]).AddHours(timezoneDifference);
        if (Request["prevEndDate"] != null) prevEndDate = Convert.ToDateTime(Request["prevEndDate"]).AddDays(1).AddSeconds(-1).AddHours(timezoneDifference);

        DataTable dt = ThermostatMonitorLib.Snapshots.LoadDeltas(thermostatId, startDate, endDate);
        DataTable data = SmoothPercents(SmoothPercents(GetPercents(dt)));
        if (Request["prevStartDate"] != null) AppendHistorical(data);

        Output(data);
    }


    private void AppendHistorical(DataTable outputDt)
    {
        includeHistorical = true;
        outputDt.Columns.Add("PrevCool", typeof(double));
        outputDt.Columns.Add("PrevHeat", typeof(double));

        DataTable dt = ThermostatMonitorLib.Snapshots.LoadDeltas(thermostatId, prevStartDate, prevEndDate);
        DataTable data = SmoothPercents(SmoothPercents(GetPercents(dt)));

        foreach (DataRow row in data.Rows)
        {
            int delta = Convert.ToInt32(row["Delta"]);
            double cool = Convert.ToDouble(row["Cool"]);
            double heat = Convert.ToDouble(row["Heat"]);
            DataRow existingRow = GetRowByDelta(outputDt, delta);
            if (existingRow != null)
            {
                existingRow["PrevCool"] = cool;
                existingRow["PrevHeat"] = heat;
            }
        }
    }


    private DataRow GetRowByDelta(DataTable dt, int delta)
    {
        foreach (DataRow row in dt.Rows)
        {
            int rowDelta = Convert.ToInt32(row["delta"]);
            if (rowDelta == delta) return row;
        }
        return null;
    }

    private DataTable GetPercents(DataTable dt)
    {
        DataTable result = new DataTable();
        result.Columns.Add("Delta", typeof(int));
        result.Columns.Add("Heat", typeof(double));
        result.Columns.Add("Cool", typeof(double));


        for (int i = -100; i < 100; i++)
        {
            DataTable deltaRows = GetRowsByDelta(i, dt);
            if (deltaRows.Rows.Count > 0)
            {
                int heatSeconds = GetTotalSeconds("Heat", deltaRows);
                int coolSeconds = GetTotalSeconds("Cool", deltaRows);
                int totalSeconds = GetTotalSeconds("", deltaRows);

                double heatPercent = (double)heatSeconds / (double)totalSeconds;
                double coolPercent = (double)coolSeconds / (double)totalSeconds;
                DataRow row = result.NewRow();
                row["Delta"] = i;
                row["Heat"] = System.Math.Round(heatPercent * 100);
                row["Cool"] = System.Math.Round(coolPercent * 100);
                result.Rows.Add(row);
            }
        }
        return result;
    }

    private DataTable SmoothPercents(DataTable dt)
    {
        DataTable result = dt.Clone();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int totalRows = 1;

            int delta = Convert.ToInt32(dt.Rows[i]["Delta"]);
            double totalHeat = Convert.ToDouble(dt.Rows[i]["Heat"]);
            double totalCool = Convert.ToDouble(dt.Rows[i]["Cool"]);

            if (i > 0)
            {
                totalRows++;
                totalHeat += Convert.ToDouble(dt.Rows[i - 1]["Heat"]);
                totalCool += Convert.ToDouble(dt.Rows[i - 1]["Cool"]);
            }
            if (i + i < dt.Rows.Count)
            {
                totalRows++;
                totalHeat += Convert.ToDouble(dt.Rows[i + 1]["Heat"]);
                totalCool += Convert.ToDouble(dt.Rows[i + 1]["Cool"]);
            }

            DataRow row = result.NewRow();
            row["Delta"] = delta;
            row["Heat"] = System.Math.Round(totalHeat / (double)totalRows, 2);
            row["Cool"] = System.Math.Round(totalCool / (double)totalRows, 2);
            result.Rows.Add(row);
        }
        return result;
    }


    private int GetTotalSeconds(String mode, DataTable dt)
    {
        int result = 0;

        foreach (DataRow row in dt.Rows)
        {
            int seconds = Convert.ToInt32(row["TotalSeconds"]);
            if (mode == "" || mode == row["Mode"].ToString()) result += seconds;
        }
        return result;

    }

    private DataTable GetRowsByDelta(int delta, DataTable dt)
    {
        DataTable result = dt.Clone();
        foreach (DataRow row in dt.Rows)
        {
            if (Convert.ToInt32(row["Delta"]) == delta) result.ImportRow(row);
        }
        return result;
    }


    private void Output(DataTable dt)
    {
        if (dt.Rows.Count == 0)
        {
            Response.Write("No data available.  This chart will display once your first thermostat cycle has been logged.");
            Response.End();
        }
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("['Delta', 'Cool', 'Heat'");
        if (includeHistorical) sb.Append(", 'Previous Cool', 'Previous Heat'");
        sb.Append("],\n\t    ");

        int i=0;
        foreach (DataRow row in dt.Rows)
        {
            if (i > 0) sb.Append(",\n\t    ");
            sb.Append("['" + row["delta"].ToString() + "°', " + row["Cool"].ToString() + ", " + row["Heat"].ToString());
            if (includeHistorical)
            {
                double prevCool = 0;
                if (!Convert.IsDBNull(row["PrevCool"])) prevCool = Convert.ToDouble(row["PrevCool"]);
                double prevHeat = 0;
                if (!Convert.IsDBNull(row["PrevHeat"])) prevHeat = Convert.ToDouble(row["PrevHeat"]);
                sb.Append(", " + prevCool.ToString() + ", " + prevHeat.ToString());
            }
            sb.Append("]");
            i++;
        }

        Data = sb.ToString();
        //Data += "var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));\n";
        //Data += "chart.draw(data, {width: 970, height: 200, scaleType:'maximized', legend:'none', hAxis: { showTextEvery:4 }, chartArea: { width:920 } });";
        //ChartDiv.Text = "<div id=\"chart_div\" style=\"width: 970px; height: 200px;\"></div>";
    }
}
