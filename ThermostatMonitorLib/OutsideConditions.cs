using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;


namespace ThermostatMonitorLib
{
    public partial class OutsideConditions
    {
        public static OutsideConditions LoadRange(int locationId, DateTime startDate, DateTime endDate)
        {
            return OutsideConditions.LoadOutsideConditions("SELECT * FROM outside_conditions WHERE location_id=@LocationId AND log_date BETWEEN @StartDate and @EndDate", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@LocationId", locationId), new MySqlParameter("@StartDate", startDate), new MySqlParameter("@EndDate", endDate) });
        }

        public static DataTable LoadSummary(int locationId, DateTime startDate, DateTime endDate, int timezoneDifference)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT DATE(DATE_ADD(log_date, INTERVAL " + timezoneDifference.ToString() + " HOUR)) as log_date,MIN(degrees) as MinDegrees,MAX(degrees) as MaxDegrees FROM outside_conditions WHERE location_id=@LocationId AND DATE(DATE_ADD(log_date, INTERVAL " + timezoneDifference.ToString() + " HOUR)) BETWEEN @StartDate AND @EndDate GROUP BY DATE(DATE_ADD(log_date, INTERVAL " + timezoneDifference.ToString() + " HOUR))", Global.MySqlConnection);
            adapter.SelectCommand.Parameters.AddWithValue("@LocationId", locationId);
            adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDate.Date);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddMilliseconds(-1));
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public string GetCSV()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LogDate", typeof(DateTime));
            dt.Columns.Add("Degrees", typeof(int));
            foreach (OutsideCondition cond in this)
            {
                DataRow row = dt.NewRow();
                row["LogDate"] = cond.LogDate;
                row["Degrees"] = cond.Degrees;
                dt.Rows.Add(row);
            }
            return Utils.DataTableToCSV(dt);
        }

        public OutsideCondition GetByTime(DateTime time)
        {
            OutsideCondition result = null;
            foreach (ThermostatMonitorLib.OutsideCondition c in this)
            {
                if (c.LogDate <= time) result = c;
            }
            return result;
        }


        public OutsideConditions GetRange(DateTime startDate, DateTime endDate)
        {
            OutsideConditions result = new OutsideConditions();
            foreach (OutsideCondition cond in this)
            {
                if (cond.LogDate >= startDate && cond.LogDate <= endDate) result.Add(cond);
            }
            return result;
        }


        public double GetTempHigh()
        {
            double result = -999;
            foreach (OutsideCondition cond in this)
            {
                if (cond.Degrees > result) result = cond.Degrees;
            }
            return result;
        }

        public double GetTempLow()
        {
            double result = 999;
            foreach (OutsideCondition cond in this)
            {
                if (cond.Degrees < result) result = cond.Degrees;
            }
            return result;
        }

        public double GetTempAverage(DateTime startTime, DateTime endTime)
        {
            int i = 0;
            double totalSeconds = 0;
            double totalDegrees = 0;
            foreach (OutsideCondition cond in this)
            {
                DateTime tempStart = cond.LogDate;
                DateTime tempEnd = endTime;
                if (startTime.Ticks > tempStart.Ticks) tempStart = startTime;
                if (this.Count > i + 1) tempEnd = this[i + 1].LogDate;
                double seconds = (tempEnd.Ticks - tempStart.Ticks) / 1000.0;
                totalSeconds += seconds;
                totalDegrees += cond.Degrees * seconds;
                i++;
            }
            return System.Math.Round(totalDegrees / totalSeconds, 1);
        }

    }
}
