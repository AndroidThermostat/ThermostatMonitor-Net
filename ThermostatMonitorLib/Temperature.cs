using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    public partial class Temperature
    {
        public static void CheckAndLogTemperature(int thermostatId, int degrees, Int16 precision)
        {
            Temperature previous = Temperature.LoadCurrentTemperature(thermostatId);
            if (previous == null || previous.Degrees != degrees) LogTemperature(thermostatId,degrees,precision);
        }

        public static Temperature LoadCurrentTemperature(int thermostatId)
        {
            Temperatures temperatures = Temperatures.LoadTemperatures("SELECT * FROM temperatures WHERE thermostat_id=@ThermostatId ORDER BY ID DESC Limit 1;", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@ThermostatId", thermostatId) });
            if (temperatures.Count == 0) return null; else return temperatures[0];
        }

        public static void LogTemperature(int thermostatId, int degrees, Int16 precision)
        {
            Temperature t = new Temperature();
            t.Degrees = degrees;
            t.LogDate = DateTime.Now;
            t.ThermostatId = thermostatId;
            t.LogPrecision = precision;
            Temperature.SaveTemperature(t);
        }
    }
}
