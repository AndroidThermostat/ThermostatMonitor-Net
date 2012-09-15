using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class TransitionStats : System.Collections.Generic.List<TransitionStat>
    {

        #region Constructor
        public TransitionStats()
        {
        }
        #endregion

        #region Methods
        public static TransitionStats LoadTransitionStats(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return TransitionStats.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static TransitionStats ConvertFromDT(DataTable dt)
        {
            TransitionStats result = new TransitionStats();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(TransitionStat.GetTransitionStat(row));
            }
            return result;
        }

        public static TransitionStats LoadAllTransitionStats()
        {
            return TransitionStats.LoadTransitionStats("LoadTransitionStatsAll", CommandType.StoredProcedure, null);
        }

        public TransitionStat GetTransitionStatById(int transitionStatId)
        {
            foreach (TransitionStat transitionStat in this)
            {
                if (transitionStat.Id == transitionStatId) return transitionStat;
            }
            return null;
        }

        public static TransitionStats LoadTransitionStatsByThermostatId(System.Int32 thermostatId)
        {
            return TransitionStats.LoadTransitionStats("LoadTransitionStatsByThermostatId", CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@ThermostatId", thermostatId) });
        }


        public TransitionStats Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            TransitionStats result = new TransitionStats();
            foreach (var i in sortedList) { result.Add((TransitionStat)i); }
            return result;
        }

        #endregion


    }
}

