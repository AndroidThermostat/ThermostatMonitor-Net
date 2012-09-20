using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;


namespace ThermostatMonitorLib
{
    public partial class Snapshots
    {

        public DataTable GetHourlyStats(int timezoneOffset)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Hour", typeof(int));
            result.Columns.Add("Cool", typeof(double));
            result.Columns.Add("Heat", typeof(double));
            int[] totalCool = new int[24];
            int[] totalHeat = new int[24];
            int[] totalSeconds = new int[24];

            foreach (Snapshot s in this)
            {
                Hashtable ht = s.GetSecondsPerHour(timezoneOffset);
                foreach (int hour in ht.Keys)
                {
                    int seconds = (int)ht[hour];
                    totalSeconds[hour] += seconds;
                    if (s.Mode == "Cool") totalCool[hour] += seconds;
                    if (s.Mode == "Heat") totalHeat[hour] += seconds;
                }
            }

            for (int i = 0; i < 24; i++)
            {
                int coolSeconds = totalCool[i];
                int heatSeconds = totalHeat[i];
                int seconds = totalSeconds[i];
                double coolPercent = 0;
                double heatPercent = 0;
                if (seconds > 0)
                {
                    coolPercent = System.Math.Round((double)coolSeconds / (double)seconds * 100, 2);
                    heatPercent = System.Math.Round((double)heatSeconds / (double)seconds * 100, 2);
                }

                DataRow row = result.NewRow();
                row["Hour"] = i;
                row["Cool"] = coolPercent;
                row["Heat"] = heatPercent;
                result.Rows.Add(row);
            }
            return result;
        }


        public static DataTable LoadDeltas(int thermostatId, DateTime startDate, DateTime endDate)
        {
            string sql = "select outside_temp_average - inside_temp_average as delta, mode, sum(seconds) as TotalSeconds from snapshots where thermostat_id=@ThermostatId and start_time BETWEEN @StartDate AND @EndDate group by outside_temp_average - inside_temp_average, mode order by delta";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, Global.MySqlConnection);
            adapter.SelectCommand.Parameters.AddWithValue("@ThermostatId", thermostatId);
            adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDate);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDate);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static void Generate(int thermostatId)
        {
            DateTime startDate = new DateTime(2000,1,1);
            Snapshot lastSnapshot = Snapshot.LoadLastSnapshot(thermostatId);

            if (lastSnapshot != null) startDate = lastSnapshot.StartTime.AddSeconds(lastSnapshot.Seconds);

            Thermostat thermostat = Thermostat.LoadThermostat(thermostatId);
            Cycles cycles = Cycles.LoadRange(thermostatId, startDate, DateTime.Now);
            Temperatures temperatures = Temperatures.LoadRange(thermostatId, startDate.AddDays(-1), DateTime.Now); //we need the previous temperature and conditions
            OutsideConditions conditions = OutsideConditions.LoadRange(thermostat.LocationId, startDate.AddDays(-1), DateTime.Now);
            cycles.RemoveIncomplete();

            if (cycles.Count == 0) return;

            DateTime lastCycleEndDate = cycles[0].StartDate;
            if (lastSnapshot != null) lastCycleEndDate = startDate;

            foreach (Cycle cycle in cycles)
            {
                try
                {
                    LogOffCycle(thermostat.Id, cycle, lastCycleEndDate, temperatures, conditions);
                }
                catch { }
                try
                {
                    LogOnCycle(thermostat.Id, cycle, temperatures, conditions);
                }
                catch { }
                lastCycleEndDate = cycle.EndDate;
            }

        }

        private static void LogOnCycle(int thermostatId, Cycle cycle, Temperatures allTemperatures, OutsideConditions allConditions)
        {
            Temperatures temperatures = allTemperatures.GetRange(cycle.StartDate, cycle.EndDate);
            OutsideConditions conditions = allConditions.GetRange(cycle.StartDate, cycle.EndDate);
            Temperature previousTemperature = allTemperatures.GetByTime(cycle.StartDate);
            OutsideCondition previousCondition = allConditions.GetByTime(cycle.StartDate);
            if (previousTemperature!=null) temperatures.Insert(0, previousTemperature);
            if (previousCondition != null) conditions.Insert(0, previousCondition);

            if (conditions.Count > 0 && temperatures.Count > 0)
            {
                Snapshot s = new Snapshot();
                s.StartTime = cycle.StartDate;
                s.Seconds = Convert.ToInt32(new TimeSpan(cycle.EndDate.Ticks - cycle.StartDate.Ticks).TotalSeconds);
                s.ThermostatId = thermostatId;
                s.Mode = cycle.CycleType;
                s.InsideTempAverage = Convert.ToInt32(temperatures.GetTempAverage(cycle.StartDate, cycle.EndDate));
                s.InsideTempHigh = Convert.ToInt32(temperatures.GetTempHigh());
                s.InsideTempLow = Convert.ToInt32(temperatures.GetTempLow());
                s.OutsideTempAverage = Convert.ToInt32(conditions.GetTempAverage(cycle.StartDate, cycle.EndDate));
                s.OutsideTempHigh = Convert.ToInt32(conditions.GetTempHigh());
                s.OutsideTempLow = Convert.ToInt32(conditions.GetTempLow());
                if (s.Seconds > 10 && s.Seconds < 86400) //if significant and less than a day
                {
                    Snapshot.SaveSnapshot(s);
                }
            }
        }

        private static void LogOffCycle(int thermostatId, Cycle cycle, DateTime lastCycleEndDate, Temperatures allTemperatures, OutsideConditions allConditions)
        {
            Temperatures temperatures = allTemperatures.GetRange(lastCycleEndDate, cycle.StartDate);
            OutsideConditions conditions = allConditions.GetRange(lastCycleEndDate, cycle.StartDate);
            Temperature previousTemperature = allTemperatures.GetByTime(lastCycleEndDate);
            OutsideCondition previousCondition = allConditions.GetByTime(lastCycleEndDate);
            temperatures.Insert(0, previousTemperature);
            conditions.Insert(0, previousCondition);

            if (cycle.StartDate <= lastCycleEndDate) return;

            if (conditions.Count > 0 && temperatures.Count > 0)
            {
                DateTime endDate = cycle.StartDate;
                Snapshot s = new Snapshot();
                s.StartTime = lastCycleEndDate;
                s.Seconds = Convert.ToInt32(new TimeSpan(cycle.StartDate.Ticks - lastCycleEndDate.Ticks).TotalSeconds);
                s.ThermostatId = thermostatId;
                s.Mode = "Off";
                s.InsideTempAverage = Convert.ToInt32(temperatures.GetTempAverage(lastCycleEndDate, cycle.StartDate));
                s.InsideTempHigh = Convert.ToInt32(temperatures.GetTempHigh());
                s.InsideTempLow = Convert.ToInt32(temperatures.GetTempLow());
                s.OutsideTempAverage = Convert.ToInt32(conditions.GetTempAverage(lastCycleEndDate, cycle.StartDate));
                s.OutsideTempHigh = Convert.ToInt32(conditions.GetTempHigh());
                s.OutsideTempLow = Convert.ToInt32(conditions.GetTempLow());
                if (s.Seconds > 10 && s.Seconds < 86400) //if significant and less than a day
                {
                    Snapshot.SaveSnapshot(s);
                }
            }
        }


        //Loads a wider range and trims off that seconds from the first and last snapshots to match the date range provided.
        public static Snapshots LoadRange(int thermostatId, DateTime startDate, DateTime endDate)
        {
            Snapshots snapshots = Snapshots.LoadSnapshots("SELECT * FROM Snapshots WHERE thermostat_id=@ThermostatId and start_time BETWEEN @StartDate AND @EndDate ORDER BY start_time", CommandType.Text, new MySqlParameter[] { 
                new MySqlParameter("@ThermostatId", thermostatId),
                new MySqlParameter("@StartDate", startDate.AddDays(-1)),
                new MySqlParameter("@EndDate", endDate)
            });

            //filter through them and chop off seconds before and after the cycle;

            Snapshots result = new Snapshots();
            foreach (Snapshot existing in snapshots)
            {
                DateTime endTime = existing.StartTime.AddSeconds(existing.Seconds);
                if (endTime > startDate)
                {
                    Snapshot snapshot = existing;
                    if (snapshot.StartTime < startDate)
                    {
                        snapshot.Seconds = snapshot.Seconds - (int)new TimeSpan(startDate.Ticks - snapshot.StartTime.Ticks).TotalSeconds;
                        snapshot.StartTime = startDate;
                    }
                    if (endTime > endDate)
                    {
                        snapshot.Seconds = snapshot.Seconds - (int)new TimeSpan(endTime.Ticks - endDate.Ticks).TotalSeconds;
                    }
                    result.Add(snapshot);
                }
            }
            return result;
        }


    }
}
