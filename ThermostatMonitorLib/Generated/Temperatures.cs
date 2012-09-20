
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Temperatures : System.Collections.Generic.List<Temperature>
    {

        #region Constructor
        public Temperatures()
        {
        }
        #endregion

        #region Methods
        public static Temperatures LoadTemperatures(string sql, System.Data.CommandType commandType, MySqlParameter[] parameters)
        {
            return Temperatures.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static Temperatures ConvertFromDT(DataTable dt)
        {
            Temperatures result = new Temperatures();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(Temperature.GetTemperature(row));
            }
            return result;
        }

        public static Temperatures LoadAllTemperatures()
        {
            return Temperatures.LoadTemperatures("temperatures_load_all", CommandType.StoredProcedure, null);
        }

        public Temperature GetTemperatureById(int temperatureId)
        {
            foreach (Temperature temperature in this)
            {
                if (temperature.Id == temperatureId) return temperature;
            }
            return null;
        }

        public static Temperatures LoadTemperaturesByThermostatId(int thermostatId)
        {
            return Temperatures.LoadTemperatures("temperatures_load_by_thermostat_id", CommandType.StoredProcedure, new MySqlParameter[] { new MySqlParameter("@thermostat_id", thermostatId) });
        }


        public Temperatures Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            Temperatures result = new Temperatures();
            foreach (var i in sortedList) { result.Add((Temperature)i); }
            return result;
        }

        #endregion

    }
}


