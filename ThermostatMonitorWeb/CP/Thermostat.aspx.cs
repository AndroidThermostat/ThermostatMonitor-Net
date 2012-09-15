using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class cp_Thermostat : System.Web.UI.Page
{
    public int TimezoneDifference = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request["Id"]);
        ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(id);
        ThermostatMonitorLib.Location location = ThermostatMonitorLib.Location.LoadLocation(thermostat.LocationId);
        if (location.UserId != AppUser.Current.UserData.Id) Response.Redirect("/login.aspx");


        TimezoneDifference = AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings);



        ThermostatName.Text = thermostat.DisplayName + " (" + thermostat.IpAddress + ")";
        //ThermostatMonitorLib.Cycle currentCycle=ThermostatMonitorLib.Cycle.LoadOpenCycle(thermostat.Id);

        ThermostatMonitorLib.OutsideCondition currentCondition=ThermostatMonitorLib.OutsideCondition.LoadCurrentCondition(location.Id);
        ThermostatMonitorLib.Temperature currentTemperature = ThermostatMonitorLib.Temperature.LoadCurrentTemperature(thermostat.Id);
        CurrentConditionsLit.Text = "Currently: ";
        //if (currentCycle == null) CurrentConditionsLit.Text += "Off"; else CurrentConditionsLit.Text += currentCycle.CycleType;
        if (currentTemperature != null && currentCondition != null)
        {
            CurrentConditionsLit.Text += " - Inside: " + currentTemperature.Degrees.ToString() + "°";
            CurrentConditionsLit.Text += " - Outside: " + currentCondition.Degrees.ToString() + "°";
        }
        else
        {
            CurrentConditionsLit.Text = "No data has been gathered for this thermostat.";
        }




        ThermostatMonitorLib.Cycles cycles = ThermostatMonitorLib.Cycles.LoadRange(id, DateTime.Now.AddDays(-1).AddHours(AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings)), DateTime.Now.AddHours(AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings)));


        DataTable dt = ThermostatMonitorLib.Cycles.LoadFullSummary(thermostat.LocationId, thermostat.Id, DateTime.Today.AddDays(-7), DateTime.Today, AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings));
        HistorySummary1.Populate(dt,thermostat);

    }
}
