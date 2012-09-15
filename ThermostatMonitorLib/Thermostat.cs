using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    public partial class Thermostat
    {

        public static Thermostat LoadByKeyName(string keyName)
        {
            Thermostats thermostats = Thermostats.LoadThermostats("SELECT * FROM Thermostats WHERE KeyName=@KeyName", CommandType.Text, new SqlParameter[] { new SqlParameter("@KeyName", keyName) });
            if (thermostats.Count == 0) return null; else return thermostats[0];
        }

        public static Thermostat LoadThermostat(int locationId, string ipAddress)
        {
            Thermostats thermostats = Thermostats.LoadThermostats("SELECT * FROM Thermostats WHERE LocationId=@LocationId AND IpAddress=@IpAddress", CommandType.Text, new SqlParameter[] { new SqlParameter("@LocationId", locationId), new SqlParameter("@IpAddress",ipAddress) });
            if (thermostats.Count == 0) return null; else return thermostats[0];
        }
    }
}
