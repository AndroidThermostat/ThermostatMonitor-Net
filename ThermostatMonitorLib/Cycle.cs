using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    public partial class Cycle
    {
        public static void LogCycle(int thermostatId, string cycleType, DateTime startDate, DateTime endDate, Int16 startPrecision, Int16 endPrecision)
        {
            Cycle c = new Cycle();
            c.CycleType = cycleType;
            c.EndDate = endDate;
            c.StartDate = startDate;
            c.ThermostatId = thermostatId;
            c.StartPrecision = startPrecision;
            c.EndPrecision = endPrecision;
            Cycle.SaveCycle(c);
        }

        public static void StartCycle(int thermostatId, string cycleType, Int16 precision)
        {
            Cycle c = new Cycle();
            c.CycleType = cycleType;
            c.StartDate = DateTime.Now;
            c.IsEndDateNull = true;
            c.ThermostatId = thermostatId;
            c.StartPrecision = precision;
            Cycle.SaveCycle(c);
        }

        public static void EndCycle(Cycle c, Int16 precision)
        {
            c.EndDate = DateTime.Now;
            c.EndPrecision = precision;
            Cycle.SaveCycle(c);
        }

        public static void EndOpenCycle(int thermostatId, Int16 precision)
        {
            Cycle c = LoadOpenCycle(thermostatId);
            if (c != null) EndCycle(c, precision);
        }

        public static Cycle LoadOpenCycle(int thermostatId)
        {
            Cycles cycles = Cycles.LoadCycles("SELECT * FROM cycles WHERE thermostat_id=@ThermostatId and end_date IS NULL", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@ThermostatId", thermostatId) });
            if (cycles.Count == 0) return null; else return cycles[0];
        }


    }
}
