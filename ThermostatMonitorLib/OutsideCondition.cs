using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    public partial class OutsideCondition
    {
        public static void Log(int locationId, int degrees)
        {
            ThermostatMonitorLib.OutsideCondition oc = new OutsideCondition();
            oc.Degrees = degrees;
            oc.LocationId = locationId;
            oc.LogDate = DateTime.Now;
            ThermostatMonitorLib.OutsideCondition.SaveOutsideCondition(oc);
        }

        public static OutsideCondition LoadCurrentCondition(int locationId)
        {
            OutsideConditions conditions = OutsideConditions.LoadOutsideConditions("SELECT * FROM outside_conditions WHERE location_id=@LocationId ORDER BY ID DESC limit 1;", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@LocationId", locationId) });
            if (conditions.Count == 0) return null; else return conditions[0];
        }

        public static void CheckAndLog(int locationId, int degrees)
        {
            OutsideCondition previous = OutsideCondition.LoadCurrentCondition(locationId);
            if (previous == null || previous.Degrees != degrees) Log(locationId, degrees);
        }

        

    }
}
