using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    public partial class Thermostat
    {

        public static Thermostat LoadByKeyName(string keyName)
        {
            Thermostats thermostats = Thermostats.LoadThermostats("SELECT * FROM thermostats WHERE key_name=@KeyName", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@KeyName", keyName) });
            if (thermostats.Count == 0) return null; else return thermostats[0];
        }

        public static Thermostat LoadThermostat(int locationId, string ipAddress)
        {
            Thermostats thermostats = Thermostats.LoadThermostats("SELECT * FROM thermostats WHERE location_id=@LocationId AND ip_address=@IpAddress", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@LocationId", locationId), new MySqlParameter("@IpAddress", ipAddress) });
            if (thermostats.Count == 0) return null; else return thermostats[0];
        }
    }
}
