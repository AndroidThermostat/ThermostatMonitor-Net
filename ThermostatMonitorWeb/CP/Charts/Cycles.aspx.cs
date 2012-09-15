using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class cp_Charts_Cycles : System.Web.UI.Page
{
    public string Data = "";
    public int TimezoneDifference = 0;
    DateTime startTime = DateTime.Now.AddDays(-1);
    DateTime endTime = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        int thermostatId = Convert.ToInt32(Request["thermostatId"]);
        TimezoneDifference = Convert.ToInt32(Request["TimezoneDifference"]);
        if (Request["date"] != null)
        {
            startTime = Convert.ToDateTime(Request["Date"]);
            endTime = startTime.AddDays(1).AddMilliseconds(-1);
            startTime = startTime.AddHours(-TimezoneDifference);
            endTime = endTime.AddHours(-TimezoneDifference);
            if (endTime > DateTime.Now) endTime = DateTime.Now;
        }


        Populate(thermostatId);
    }



    private void Populate(int thermostatId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LogDate", typeof(DateTime));
        dt.Columns.Add("AC_On", typeof(int));
        ThermostatMonitorLib.Cycles cycles = ThermostatMonitorLib.Cycles.LoadRange(thermostatId, startTime, endTime);

        if (cycles.Count > 0)
        {
            DataRow row = dt.NewRow();
            row[0] = startTime;
            row[1] = 0;
            dt.Rows.Add(row);
        }
        foreach (ThermostatMonitorLib.Cycle cycle in cycles)
        {
            if (cycle.IsEndDateNull) cycle.EndDate = endTime;
            DataRow row = dt.NewRow();
            row[0] = cycle.StartDate.AddSeconds(-1);
            row[1] = 0;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = cycle.StartDate;
            row[1] = 1;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = cycle.EndDate.AddSeconds(-1);
            row[1] = 1;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = cycle.EndDate;
            row[1] = 0;
            dt.Rows.Add(row);
        }

        foreach (DataRow row in dt.Rows)
        {
            DateTime theDate = Convert.ToDateTime(row[0]);
            if (theDate < startTime) theDate = startTime;
            if (theDate > endTime) theDate = endTime;
            row[0] = theDate;
        }

        if (cycles.Count > 0)
        {
            DataRow row = dt.NewRow();
            row[0] = endTime;
            row[1] = 0;
            dt.Rows.Add(row);
        }

        Output(dt);
    }

    public void Output(DataTable dt)
    {
        dt.DefaultView.Sort = "LogDate";
        //FillInDataTable(dt);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("data.addRows(" + dt.Rows.Count + ");\n");
        for (int i = 0; i < dt.DefaultView.Count; i++)
        {
            DataRowView row = dt.DefaultView[i];//.Rows[i];
            DateTime adjustedDate = Convert.ToDateTime(row[0]).AddHours(TimezoneDifference);
            sb.Append("data.setValue(" + i.ToString() + ",0,new Date(" + adjustedDate.ToString("yyyy,") + Convert.ToString(adjustedDate.Month - 1) + adjustedDate.ToString(",d,H,m") + "));\n");
            if (!Convert.IsDBNull(row[1])) sb.Append("data.setValue(" + i.ToString() + ",1," + row[1].ToString() + ");\n");
        }


        DateTime adjustedStartTime = startTime.AddHours(TimezoneDifference);
        DateTime adjustedEndTime = endTime.AddHours(TimezoneDifference);

        Data = sb.ToString();
        Data += "var chart = new google.visualization.AnnotatedTimeLine(document.getElementById('chart_div'));\n";
        Data += "var startTime=new Date(" + adjustedStartTime.ToString("yyyy,") + Convert.ToString(adjustedStartTime.Month - 1) + adjustedStartTime.ToString(",d,H,m") + ");";
        Data += "var endTime=new Date(" + adjustedEndTime.ToString("yyyy,") + Convert.ToString(adjustedEndTime.Month - 1) + adjustedEndTime.ToString(",d,H,m") + ");";
        Data += "chart.draw(data, {width: 970, height: 100, interpolateNulls: true, pointSize: 1, displayLegendDots:false, displayLegendValues:false, displayRangeSelector:false, displayZoomButtons:false, fill:100, dateFormat:'h:mm', scaleType:'maximized', zoomStartTime:startTime });";
        ChartDiv.Text = "<div id=\"chart_div\" style=\"width: 970px; height: 100px;\"></div>";
    }


}
