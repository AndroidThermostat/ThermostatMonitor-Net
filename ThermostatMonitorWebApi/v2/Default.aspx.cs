using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThermostatMonitorWebApi.v2
{
    public partial class Default : System.Web.UI.Page
    {
        String key = "";
        String action = "";
        String mode = "";
        String zip = "";
        int duration = 0;
        short precision = 0;
        int temperature = 0;

        ThermostatMonitorLib.Thermostat thermostat;

        protected void Page_Load(object sender, EventArgs e)
        {
            key = Request["k"];
            action = Request["a"];
            mode = Request["m"];
            duration = Convert.ToInt32(Request["d"]);
            precision = Convert.ToInt16(Request["p"]);
            temperature = (int)Convert.ToDouble(Request["t"]);

            if (key!=null && key!="") thermostat = ThermostatMonitorLib.Thermostat.LoadByKeyName(key);
            if (action == "location")
            {
                ReturnLocation();
            }
            else
            {
                if (thermostat != null)
                {
                    switch (action)
                    {
                        case "cycle":
                            LogCycle();
                            break;
                        case "temp":
                            LogTemperatureChange();
                            break;
                        case "conditions":
                            LogConditions();
                            break;
                        case "stats":
                            RedirectToStats();
                            break;
                    }
                }
            }
        }

        private void ReturnLocation()
        {
            int result = ThermostatMonitorLib.Weather.GetCityId(Request["z"]);
            Response.Write(result.ToString());
            Response.End();
        }

        private void RedirectToStats()
        {
            ThermostatMonitorLib.Location l = ThermostatMonitorLib.Location.LoadLocation(thermostat.LocationId);
            ThermostatMonitorLib.User u = ThermostatMonitorLib.User.LoadUser(l.UserId);
            String url = "http://www.thermostatmonitor.com/cp/thermostat.aspx?id=" + thermostat.Id.ToString() + "&authCode=" + u.AuthCode;
            Response.Redirect(url);
        }
        
        private void LogConditions()
        {
            ThermostatMonitorLib.OutsideCondition.Log(thermostat.LocationId, temperature);
        }

        private void LogTemperatureChange()
        {
            ThermostatMonitorLib.Temperature.LogTemperature(thermostat.Id, temperature, precision);
        }

        private void LogCycle()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.AddSeconds(-duration);
            ThermostatMonitorLib.Cycle.LogCycle(thermostat.Id, mode, startDate, endDate, precision, precision);
            ThermostatMonitorLib.Snapshots.Generate(thermostat.Id);
        }

    }
}
