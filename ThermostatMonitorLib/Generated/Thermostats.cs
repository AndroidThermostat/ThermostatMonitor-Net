using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Thermostats : System.Collections.Generic.List<Thermostat>
    {

        #region Constructor
        public Thermostats()
        {
        }
        #endregion

        #region Methods
        public static Thermostats LoadThermostats(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return Thermostats.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static Thermostats ConvertFromDT(DataTable dt)
        {
            Thermostats result = new Thermostats();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(Thermostat.GetThermostat(row));
            }
            return result;
        }

        public static Thermostats LoadAllThermostats()
        {
            return Thermostats.LoadThermostats("LoadThermostatsAll", CommandType.StoredProcedure, null);
        }

        public Thermostat GetThermostatById(int thermostatId)
        {
            foreach (Thermostat thermostat in this)
            {
                if (thermostat.Id == thermostatId) return thermostat;
            }
            return null;
        }

        public static Thermostats LoadThermostatsByLocationId(System.Int32 locationId)
        {
            return Thermostats.LoadThermostats("LoadThermostatsByLocationId", CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@LocationId", locationId) });
        }


        public Thermostats Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            Thermostats result = new Thermostats();
            foreach (var i in sortedList) { result.Add((Thermostat)i); }
            return result;
        }

        #endregion


    }
}

