using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        ThermostatMonitorLib.Locations locations = ThermostatMonitorLib.Locations.LoadLocationsByUserId(AppUser.Current.UserData.Id);
        foreach (ThermostatMonitorLib.Location location in locations)
        {
            sb.Append("<tr><td><a href=\"location.aspx?id=" + location.Id.ToString() + "\">" + location.Name.ToString() + "</a></td><td> - API Key: <b>" + location.ApiKey + "</b></td></tr>");
            ThermostatMonitorLib.Thermostats thermostats = ThermostatMonitorLib.Thermostats.LoadThermostatsByLocationId(location.Id);
            foreach (ThermostatMonitorLib.Thermostat thermostat in thermostats)
            {
                sb.Append("<tr><td></td><td> - <a href=\"thermostat.aspx?id=" + thermostat.Id.ToString() + "\">" + thermostat.DisplayName + "</a> [<a href=\"editthermostat.aspx?id=" + thermostat.Id.ToString() + "\">Edit</a>]</td></tr>");
            }
        }
        ThermostatsLit.Text = sb.ToString();
    }
}
