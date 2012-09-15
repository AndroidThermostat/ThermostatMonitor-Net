using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
            OutsideConditions conditions = OutsideConditions.LoadOutsideConditions("SELECT top 1 * FROM OutsideConditions WHERE LocationId=@LocationId ORDER BY ID DESC", CommandType.Text, new SqlParameter[] { new SqlParameter("@LocationId", locationId) });
            if (conditions.Count == 0) return null; else return conditions[0];
        }

        public static void CheckAndLog(int locationId, int degrees)
        {
            OutsideCondition previous = OutsideCondition.LoadCurrentCondition(locationId);
            if (previous == null || previous.Degrees != degrees) Log(locationId, degrees);
        }

        

    }
}
