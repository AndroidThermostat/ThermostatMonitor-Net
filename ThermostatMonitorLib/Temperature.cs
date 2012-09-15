using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
            Temperatures temperatures=Temperatures.LoadTemperatures("SELECT TOP 1 * FROM Temperatures WHERE ThermostatId=@ThermostatId ORDER BY ID DESC", CommandType.Text, new SqlParameter[] { new SqlParameter("@ThermostatId", thermostatId) });
            if (temperatures.Count == 0) return null; else return temperatures[0];
        }

        public static void LogTemperature(int thermostatId, int degrees, Int16 precision)
        {
            Temperature t = new Temperature();
            t.Degrees = degrees;
            t.LogDate = DateTime.Now;
            t.ThermostatId = thermostatId;
            t.Precision = precision;
            Temperature.SaveTemperature(t);
        }
    }
}
