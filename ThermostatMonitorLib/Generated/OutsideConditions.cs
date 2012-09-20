
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class OutsideConditions : System.Collections.Generic.List<OutsideCondition>
    {

        #region Constructor
        public OutsideConditions()
        {
        }
        #endregion

        #region Methods
        public static OutsideConditions LoadOutsideConditions(string sql, System.Data.CommandType commandType, MySqlParameter[] parameters)
        {
            return OutsideConditions.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static OutsideConditions ConvertFromDT(DataTable dt)
        {
            OutsideConditions result = new OutsideConditions();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(OutsideCondition.GetOutsideCondition(row));
            }
            return result;
        }

        public static OutsideConditions LoadAllOutsideConditions()
        {
            return OutsideConditions.LoadOutsideConditions("outside_conditions_load_all", CommandType.StoredProcedure, null);
        }

        public OutsideCondition GetOutsideConditionById(int outsideConditionId)
        {
            foreach (OutsideCondition outsideCondition in this)
            {
                if (outsideCondition.Id == outsideConditionId) return outsideCondition;
            }
            return null;
        }

        public static OutsideConditions LoadOutsideConditionsByLocationId(int locationId)
        {
            return OutsideConditions.LoadOutsideConditions("outside_conditions_load_by_location_id", CommandType.StoredProcedure, new MySqlParameter[] { new MySqlParameter("@location_id", locationId) });
        }


        public OutsideConditions Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            OutsideConditions result = new OutsideConditions();
            foreach (var i in sortedList) { result.Add((OutsideCondition)i); }
            return result;
        }

        #endregion

    }
}


