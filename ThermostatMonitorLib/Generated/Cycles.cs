


using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;


namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Cycles : System.Collections.Generic.List<Cycle>
    {

        #region Constructor
        public Cycles()
        {
        }
        #endregion

        #region Methods
        public static Cycles LoadCycles(string sql, System.Data.CommandType commandType, MySqlParameter[] parameters)
        {
            return Cycles.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static Cycles ConvertFromDT(DataTable dt)
        {
            Cycles result = new Cycles();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(Cycle.GetCycle(row));
            }
            return result;
        }

        public static Cycles LoadAllCycles()
        {
            return Cycles.LoadCycles("cycles_load_all", CommandType.StoredProcedure, null);
        }

        public Cycle GetCycleById(int cycleId)
        {
            foreach (Cycle cycle in this)
            {
                if (cycle.Id == cycleId) return cycle;
            }
            return null;
        }

        public static Cycles LoadCyclesByThermostatId(int thermostatId)
        {
            return Cycles.LoadCycles("cycles_load_by_thermostat_id", CommandType.StoredProcedure, new MySqlParameter[] { new MySqlParameter("@thermostat_id", thermostatId) });
        }


        public Cycles Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            Cycles result = new Cycles();
            foreach (var i in sortedList) { result.Add((Cycle)i); }
            return result;
        }

        #endregion

    }
}


