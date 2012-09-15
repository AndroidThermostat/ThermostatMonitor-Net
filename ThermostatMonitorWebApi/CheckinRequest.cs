using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ThermostatMonitorWebApi
{
    public class CheckinRequest
    {
        public Thermostats Thermostats;
        public string ZipCode;

        public static CheckinRequest Load(ThermostatMonitorLib.Location location, string json)
        {
            CheckinRequest result = new CheckinRequest();

            result.ZipCode = location.ZipCode;
            result.Thermostats = new Thermostats();

            Hashtable hash = (Hashtable)ThermostatMonitorLib.JSON.JsonDecode(json);

            ArrayList thermostats = new ArrayList();
            try
            {
                thermostats = (ArrayList)hash["thermostats"];
            }
            catch { }

            foreach (System.Collections.Hashtable thermostatHash in thermostats)
            {
                Thermostat thermostat = new Thermostat();
                thermostat.State = thermostatHash["state"].ToString();
                thermostat.IpAddress = thermostatHash["ipAddress"].ToString();
                thermostat.Temperature = Convert.ToInt32(thermostatHash["temperature"]);
                thermostat.CheckinTime = DateTime.Now;
                result.Thermostats.Add(thermostat);
            }
            return result;
        }

    }
}
