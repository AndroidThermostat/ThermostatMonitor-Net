using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class cp_Charts_Hours : System.Web.UI.Page
{
    public string Data = "";
    public int TimezoneDifference = 0;
    DateTime startDate = new DateTime(2000,1,1);
    DateTime endDate = DateTime.Now;
    DateTime prevStartDate = new DateTime(2000,1,1);
    DateTime prevEndDate = DateTime.Now;
    int thermostatId;
    bool includeHistorical = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        thermostatId=Convert.ToInt32(Request["thermostatId"]);
        TimezoneDifference = Convert.ToInt32(Request["TimezoneDifference"]);

        if (Request["startDate"] != null) startDate = Convert.ToDateTime(Request["startDate"]).AddHours(TimezoneDifference);
        if (Request["endDate"] != null) endDate = Convert.ToDateTime(Request["endDate"]).AddDays(1).AddSeconds(-1).AddHours(TimezoneDifference);

        if (Request["prevStartDate"] != null) prevStartDate = Convert.ToDateTime(Request["prevStartDate"]).AddHours(TimezoneDifference);
        if (Request["prevEndDate"] != null) prevEndDate = Convert.ToDateTime(Request["prevEndDate"]).AddDays(1).AddSeconds(-1).AddHours(TimezoneDifference);


        ThermostatMonitorLib.Snapshots snapshots = ThermostatMonitorLib.Snapshots.LoadRange(thermostatId, startDate, endDate);
        DataTable dt = snapshots.GetHourlyStats(TimezoneDifference);
        if (Request["prevStartDate"] != null) AppendHistorical(dt);

        Output(dt);
    }

    private void AppendHistorical(DataTable outputDt)
    {
        includeHistorical = true;
        outputDt.Columns.Add("PrevCool", typeof(double));
        outputDt.Columns.Add("PrevHeat", typeof(double));

        ThermostatMonitorLib.Snapshots snapshots = ThermostatMonitorLib.Snapshots.LoadRange(thermostatId, prevStartDate, prevEndDate);
        DataTable dt = snapshots.GetHourlyStats(TimezoneDifference);

        foreach (DataRow row in dt.Rows)
        {
            int hour = Convert.ToInt32(row["Hour"]);
            double cool = Convert.ToDouble(row["Cool"]);
            double heat = Convert.ToDouble(row["Heat"]);
            DataRow existingRow = GetRowByHour(outputDt, hour);
            existingRow["PrevCool"] = cool;
            existingRow["PrevHeat"] = heat;
        }

    }

    private DataRow GetRowByHour(DataTable dt, int hour)
    {
        foreach (DataRow row in dt.Rows)
        {
            int rowHour = Convert.ToInt32(row["hour"]);
            if (rowHour == hour) return row;
        }
        return null;
    }


    private void Output(DataTable dt)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("['Hour', 'Cool', 'Heat'");
        if (includeHistorical) sb.Append(", 'Previous Cool', 'Previous Heat'");
        sb.Append("],\n\t    ");
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            double prevCool = 0;
            double prevHeat = 0;

            int hour = Convert.ToInt32(row["Hour"]);
            double cool = Convert.ToDouble(row["Cool"]);
            double heat = Convert.ToDouble(row["Heat"]);
            if (includeHistorical)
            {
                prevCool = Convert.ToDouble(row["PrevCool"]);
                prevHeat = Convert.ToDouble(row["PrevHeat"]);
            }
            
            string displayTime = "";
            if (hour < 13) displayTime = hour.ToString() + "am"; else displayTime = Convert.ToInt32(hour - 12).ToString() + "pm";
            if (displayTime == "12am") displayTime = "12pm";
            if (displayTime == "0am") displayTime = "12am";


            if (i > 0) sb.Append(",\n\t    ");
            sb.Append("['" + displayTime + "', " + cool.ToString() + ", " + heat.ToString());
            if (includeHistorical) sb.Append(", " + prevCool.ToString() + ", " + prevHeat.ToString());
            sb.Append("]");
            i++;
            Data = sb.ToString();
        }

    }

}
