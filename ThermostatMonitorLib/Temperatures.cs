using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    public partial class Temperatures
    {
        public static Temperatures LoadRange(System.Int32 thermostatId,DateTime startDate, DateTime endDate)
        {
            return Temperatures.LoadTemperatures("SELECT * FROM Temperatures WHERE ThermostatID=@ThermostatId AND LogDate BETWEEN @StartDate and @EndDate", CommandType.Text, new SqlParameter[] { new SqlParameter("@ThermostatId", thermostatId), new SqlParameter("@StartDate",startDate), new SqlParameter("@EndDate",endDate) });
        }

        public Temperatures GetRange(DateTime startDate, DateTime endDate)
        {
            Temperatures result = new Temperatures();
            foreach (Temperature temp in this)
            {
                if (temp.LogDate >= startDate && temp.LogDate <= endDate) result.Add(temp);
            }
            return result;
        }

        public double GetTempHigh()
        {
            double result = -999;
            foreach (Temperature temp in this)
            {
                if (temp.Degrees > result) result = temp.Degrees;
            }
            return result;
        }

        public double GetTempLow()
        {
            double result = 999;
            foreach (Temperature temp in this)
            {
                if (temp.Degrees < result) result = temp.Degrees;
            }
            return result;
        }

        public double GetTempAverage(DateTime startTime, DateTime endTime)
        {
            int i=0;
            double totalSeconds = 0;
            double totalDegrees = 0;
            foreach (Temperature temp in this)
            {
                DateTime tempStart = temp.LogDate;
                DateTime tempEnd = endTime;
                if (startTime.Ticks > tempStart.Ticks) tempStart = startTime;
                if (this.Count > i + 1) tempEnd = this[i + 1].LogDate;
                double seconds = (tempEnd.Ticks - tempStart.Ticks) / 1000.0;
                totalSeconds += seconds;
                totalDegrees += temp.Degrees * seconds;
                i++;
            }
            double result = System.Math.Round(totalDegrees / totalSeconds, 1);
            return result;
        }

        public string GetCSV()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LogDate", typeof(DateTime));
            dt.Columns.Add("Degrees", typeof(int));
            foreach (Temperature temp in this)
            {
                DataRow row = dt.NewRow();
                row["LogDate"] = temp.LogDate;
                row["Degrees"] = temp.Degrees;
                dt.Rows.Add(row);
            }
            return Utils.DataTableToCSV(dt);
        }

        public Temperature GetByTime(DateTime time)
        {
            Temperature result = null;
            foreach (ThermostatMonitorLib.Temperature t in this)
            {
                if (t.LogDate <= time) result = t;
            }
            return result;
        }

    }
}
