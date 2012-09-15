using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Collections;

namespace ThermostatMonitorWebApi
{
    public class Thermostats:List<Thermostat>
    {
        private static Hashtable _cache = new Hashtable();
        public static Hashtable Cache
        {
            get { return _cache; }
        }

        public static void CleanCache()
        {
            DateTime minTime=DateTime.Now.AddMinutes(-10);

            String[] keys = new String[Cache.Keys.Count];
                
            Cache.Keys.CopyTo(keys, 0);

            foreach (string key in keys)
            {
                Thermostat t = (Thermostat)Cache[key];
                if (t.CheckinTime < minTime)
                {
                    if (t.State != "Off")
                    {
                        string[] parts=key.Split('_');
                        int locationId=Convert.ToInt32(parts[0]);
                        ThermostatMonitorLib.Thermostat thermostat=ThermostatMonitorLib.Thermostat.LoadThermostat(locationId, t.IpAddress);
                        ThermostatMonitorLib.Cycle.EndOpenCycle(thermostat.Id, (short)-1);
                    }
                    Cache.Remove(key);
                }
            }
        }
    }
}
