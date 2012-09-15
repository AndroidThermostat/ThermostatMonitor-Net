using System;
using System.Data;
using System.Data.SqlClient;
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
        public static Temperatures LoadTemperatures(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
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
            return Temperatures.LoadTemperatures("LoadTemperaturesAll", CommandType.StoredProcedure, null);
        }

        public Temperature GetTemperatureById(int temperatureId)
        {
            foreach (Temperature temperature in this)
            {
                if (temperature.Id == temperatureId) return temperature;
            }
            return null;
        }

        public static Temperatures LoadTemperaturesByThermostatId(System.Int32 thermostatId)
        {
            return Temperatures.LoadTemperatures("LoadTemperaturesByThermostatId", CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@ThermostatId", thermostatId) });
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

