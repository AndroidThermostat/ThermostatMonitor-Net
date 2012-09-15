using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class cp_CsvDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int thermostatId = Convert.ToInt32(Request["ThermostatId"]);
        ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(thermostatId);
        ThermostatMonitorLib.Location location = ThermostatMonitorLib.Location.LoadLocation(thermostat.LocationId);
        if (location.UserId != AppUser.Current.UserData.Id) Response.Redirect("/cp/");

        string reportType = Convert.ToString(Request["ReportType"]);
        switch (reportType)
        {
            case "summary":
                OutputSummary(thermostat,location);
                break;
            case "cycles":
                OutputCycles(thermostat, location);
                break;
        }

    }

    private void OutputSummary(ThermostatMonitorLib.Thermostat thermostat,ThermostatMonitorLib.Location location)
    {
        DataTable dt = ThermostatMonitorLib.Cycles.LoadFullSummary(thermostat.LocationId, thermostat.Id, new DateTime(2000, 1, 1), DateTime.Today, AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings));
        DataTable resultDt = new DataTable();
        foreach (DataColumn dc in dt.Columns)
        {
            resultDt.Columns.Add(dc.ColumnName.Replace("Seconds", "Minutes"), dc.DataType);
        }
        foreach (DataRow row in dt.Rows)
        {
            DataRow newRow = resultDt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName.Contains("Seconds"))
                {
                    double minutes = 0;
                    try { minutes = System.Math.Round(Convert.ToDouble(row[dc.ColumnName]) / 60, 2); }
                    catch { }
                    newRow[dc.ColumnName.Replace("Seconds", "Minutes")] = minutes;
                }
                else
                {
                    newRow[dc.ColumnName] = row[dc.ColumnName];
                }
            }
            resultDt.Rows.Add(newRow);
        }

        OutputCSV(ThermostatMonitorLib.Utils.DataTableToCSV(resultDt), "summary.csv");
    }

    private void OutputCycles(ThermostatMonitorLib.Thermostat thermostat, ThermostatMonitorLib.Location location)
    {

        ThermostatMonitorLib.Cycles cycles=ThermostatMonitorLib.Cycles.LoadRange(thermostat.Id, new DateTime(2000, 1, 1), DateTime.Now);
        OutputCSV(cycles.GetCSV(thermostat.ACKilowatts, thermostat.FanKilowatts, thermostat.HeatBtuPerHour, AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings)), "cycles.csv");
    }

    private void OutputCSV(string csv, string fileName)
    {
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.ContentType = "text/csv";
        Response.Write(csv);
        Response.End();
    }


}
