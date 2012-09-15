using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    public partial class Cycles
    {
        public static Cycles LoadRange(System.Int32 thermostatId, DateTime startDate, DateTime endDate)
        {
            return Cycles.LoadCycles("SELECT * FROM Cycles WHERE ThermostatID=@ThermostatId AND StartDate BETWEEN @StartDate and @EndDate", CommandType.Text, new SqlParameter[] { new SqlParameter("@ThermostatId", thermostatId), new SqlParameter("@StartDate", startDate), new SqlParameter("@EndDate", endDate) });
        }

        public void RemoveIncomplete()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this[i].IsEndDateNull) this.RemoveAt(i);
            }
        }

        public static DataTable LoadFullSummary(int locationId, int thermostatId, DateTime startDate, DateTime endDate, int timezoneDifference)
        {
            DataTable cycles = LoadSummary(thermostatId, startDate, endDate,timezoneDifference);
            DataTable weather = OutsideConditions.LoadSummary(locationId, startDate, endDate, timezoneDifference);

            DataTable result = new DataTable();
            result.Columns.Add("LogDate", typeof(DateTime));
            result.Columns.Add("OutsideMin", typeof(int));
            result.Columns.Add("OutsideMax", typeof(int));
            System.Collections.Hashtable cycleTypes = new System.Collections.Hashtable();
            foreach (DataRow row in cycles.Rows) 
            {
                string cycleType = Convert.ToString(row["CycleType"]);
                if (!cycleTypes.Contains(cycleType)) cycleTypes.Add(cycleType, cycleType);
            }
            foreach (string cycleType in cycleTypes.Keys)
            {
                result.Columns.Add(cycleType + "_CycleCount", typeof(int));
                result.Columns.Add(cycleType + "_TotalSeconds", typeof(double));
                result.Columns.Add(cycleType + "_AverageSeconds", typeof(double));
            }


            System.Collections.Hashtable dateHash = new System.Collections.Hashtable();
            

            foreach (DataRow row in cycles.Rows)
            {
                string cycleType = Convert.ToString(row["CycleType"]);
                int cycleCount = Convert.ToInt32(row["CycleCount"]);
                int totalSeconds = Convert.ToInt32(row["TotalSeconds"]);
                double averageSeconds = Convert.ToDouble(totalSeconds) / Convert.ToDouble(cycleCount);
                DateTime logDate = Convert.ToDateTime(row["LogDate"]);

                bool newDate = !dateHash.Contains(logDate);
                DataRow resultRow=null;
                if (newDate) resultRow = result.NewRow(); else resultRow = result.Rows[Convert.ToInt32(dateHash[logDate])];
                resultRow[cycleType + "_CycleCount"] = cycleCount;
                resultRow[cycleType + "_TotalSeconds"] = totalSeconds;
                resultRow[cycleType + "_AverageSeconds"] = averageSeconds;
                if (newDate)
                {
                    resultRow["LogDate"] = logDate;
                    result.Rows.Add(resultRow);
                    dateHash.Add(logDate, result.Rows.Count - 1);
                }
            }

            foreach (DataRow row in weather.Rows)
            {
                DateTime logDate = Convert.ToDateTime(row["LogDate"]);
                bool newDate = !dateHash.Contains(logDate);
                if (!newDate)
                {
                    DataRow resultRow = result.Rows[Convert.ToInt32(dateHash[logDate])];
                    resultRow["OutsideMin"] = Convert.ToInt32(row["MinDegrees"]);
                    resultRow["OutsideMax"] = Convert.ToInt32(row["MaxDegrees"]);
                }
            }


            return result;

        }

        public static DataTable LoadSummary(int thermostatId, DateTime startDate, DateTime endDate, int timezoneDifference)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select SUM(datediff(ss,StartDate,EndDate)) as TotalSeconds,COUNT(*) as CycleCount,CONVERT(varchar, DateAdd(hh," + timezoneDifference.ToString() + ",StartDate), 101) as LogDate,cycletype from Cycles where ThermostatId=@ThermostatId AND DateAdd(hh," + timezoneDifference.ToString() + ",StartDate) BETWEEN @StartDate AND @EndDate and EndDate is not null group by CONVERT(varchar, DateAdd(hh," + timezoneDifference.ToString() + ",StartDate), 101),CycleType", Global.Connection);
            adapter.SelectCommand.Parameters.AddWithValue("@ThermostatId", thermostatId);
            adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDate.Date);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddMilliseconds(-1));
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public double GetTotalRunTime()
        {
            double totalRunTime = 0;
            foreach (ThermostatMonitorLib.Cycle cycle in this)
            {
                totalRunTime += TimeSpan.FromTicks(cycle.EndDate.Ticks - cycle.StartDate.Ticks).TotalMinutes;
            }
            return totalRunTime;
        }

        public string GetCSV(double acKilowatts, double fanKilowatts, double heatBTU, int timeZoneDifference)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CycleType", typeof(string));
            dt.Columns.Add("StartTime", typeof(DateTime));
            dt.Columns.Add("EndTime", typeof(DateTime));
            dt.Columns.Add("Minutes", typeof(double));
            dt.Columns.Add("kwH", typeof(double));
            dt.Columns.Add("BTUs", typeof(double));
            dt.Columns.Add("StartPrecision", typeof(Int16));
            dt.Columns.Add("EndPrecision", typeof(Int16));

            foreach (ThermostatMonitorLib.Cycle cycle in this)
            {
                if (!cycle.IsEndDateNull)
                {
                    DataRow row = dt.NewRow();
                    row["CycleType"] = cycle.CycleType;
                    row["StartTime"] = cycle.StartDate.AddHours(timeZoneDifference);
                    row["EndTime"] = cycle.EndDate.AddHours(timeZoneDifference);
                    TimeSpan ts = TimeSpan.FromTicks(cycle.EndDate.Ticks - cycle.StartDate.Ticks);
                    double minutes = ts.TotalMinutes + ts.Seconds / 60;
                    row["Minutes"] = System.Math.Round(minutes, 2);

                    switch (cycle.CycleType)
                    {
                        case "Cool":
                            row["kwH"] = System.Math.Round(minutes / 60.0 * Convert.ToDouble(acKilowatts + fanKilowatts), 2);
                            row["BTUs"] = 0;
                            break;
                        case "Heat":
                            row["kwH"] = System.Math.Round(minutes / 60.0 * Convert.ToDouble(fanKilowatts), 2);
                            row["BTUs"] = System.Math.Round(minutes / 60.0 * Convert.ToDouble(heatBTU), 2);
                            break;
                        default:
                            row["kwH"] = System.Math.Round(minutes / 60.0 * Convert.ToDouble(fanKilowatts), 2);
                            row["BTUs"] = 0;
                            break;
                    }
                    



                    
                    row["StartPrecision"] = cycle.StartPrecision;
                    row["EndPrecision"] = cycle.EndPrecision;
                    dt.Rows.Add(row);
                }
            }
            return Utils.DataTableToCSV(dt);
        }


        public Cycles GetByTime(DateTime startTime,DateTime endTime)
        {
            Cycles result = new Cycles();
            foreach (ThermostatMonitorLib.Cycle c in this)
            {
                if (c.StartDate <= endTime && c.EndDate >= startTime) result.Add(c);
            }
            return result;
        }

    }
}
