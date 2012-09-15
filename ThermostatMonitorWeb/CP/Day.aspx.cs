using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_Day : System.Web.UI.Page
{
    public int TimezoneDifference = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int thermostatId = Convert.ToInt32(Request["ThermostatId"]);
        DateTime date = Convert.ToDateTime(Request["date"]);

        ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(thermostatId);
        ThermostatMonitorLib.Location location = ThermostatMonitorLib.Location.LoadLocation(thermostat.LocationId);
        if (location.UserId != AppUser.Current.UserData.Id) Response.Redirect("/login.aspx");

        TimezoneDifference = AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings);

        ThermostatName.Text = thermostat.DisplayName;
        DateLit.Text = date.ToString("ddd, MMMM d, yyyy");



    }
}
