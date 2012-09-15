using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Snapshot
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _thermostatId;
        System.DateTime _startTime;
        System.Int32 _seconds;
        System.String _mode;
        System.Int32 _insideTempHigh;
        System.Int32 _insideTempLow;
        System.Int32 _insideTempAverage;
        System.Int32 _outsideTempHigh;
        System.Int32 _outsideTempLow;
        System.Int32 _outsideTempAverage;

        bool _isIdNull = true;
        bool _isThermostatIdNull = true;
        bool _isStartTimeNull = true;
        bool _isSecondsNull = true;
        bool _isModeNull = true;
        bool _isInsideTempHighNull = true;
        bool _isInsideTempLowNull = true;
        bool _isInsideTempAverageNull = true;
        bool _isOutsideTempHighNull = true;
        bool _isOutsideTempLowNull = true;
        bool _isOutsideTempAverageNull = true;

        #endregion

        #region Properties
        public System.Int32 Id
        {
            get { return _id; }
            set
            {
                _id = value;
                _isIdNull = false;
            }
        }

        public System.Int32 ThermostatId
        {
            get { return _thermostatId; }
            set
            {
                _thermostatId = value;
                _isThermostatIdNull = false;
            }
        }

        public System.DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                _isStartTimeNull = false;
            }
        }

        public System.Int32 Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                _isSecondsNull = false;
            }
        }

        public System.String Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                _isModeNull = false;
            }
        }

        public System.Int32 InsideTempHigh
        {
            get { return _insideTempHigh; }
            set
            {
                _insideTempHigh = value;
                _isInsideTempHighNull = false;
            }
        }

        public System.Int32 InsideTempLow
        {
            get { return _insideTempLow; }
            set
            {
                _insideTempLow = value;
                _isInsideTempLowNull = false;
            }
        }

        public System.Int32 InsideTempAverage
        {
            get { return _insideTempAverage; }
            set
            {
                _insideTempAverage = value;
                _isInsideTempAverageNull = false;
            }
        }

        public System.Int32 OutsideTempHigh
        {
            get { return _outsideTempHigh; }
            set
            {
                _outsideTempHigh = value;
                _isOutsideTempHighNull = false;
            }
        }

        public System.Int32 OutsideTempLow
        {
            get { return _outsideTempLow; }
            set
            {
                _outsideTempLow = value;
                _isOutsideTempLowNull = false;
            }
        }

        public System.Int32 OutsideTempAverage
        {
            get { return _outsideTempAverage; }
            set
            {
                _outsideTempAverage = value;
                _isOutsideTempAverageNull = false;
            }
        }


        public bool IsIdNull
        {
            get { return _isIdNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isIdNull = value;
                _id = -1;
            }
        }

        public bool IsThermostatIdNull
        {
            get { return _isThermostatIdNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isThermostatIdNull = value;
                _thermostatId = -1;
            }
        }

        public bool IsStartTimeNull
        {
            get { return _isStartTimeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isStartTimeNull = value;
                _startTime = DateTime.MinValue;
            }
        }

        public bool IsSecondsNull
        {
            get { return _isSecondsNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isSecondsNull = value;
                _seconds = -1;
            }
        }

        public bool IsModeNull
        {
            get { return _isModeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isModeNull = value;
                _mode = System.String.Empty;
            }
        }

        public bool IsInsideTempHighNull
        {
            get { return _isInsideTempHighNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isInsideTempHighNull = value;
                _insideTempHigh = -1;
            }
        }

        public bool IsInsideTempLowNull
        {
            get { return _isInsideTempLowNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isInsideTempLowNull = value;
                _insideTempLow = -1;
            }
        }

        public bool IsInsideTempAverageNull
        {
            get { return _isInsideTempAverageNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isInsideTempAverageNull = value;
                _insideTempAverage = -1;
            }
        }

        public bool IsOutsideTempHighNull
        {
            get { return _isOutsideTempHighNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isOutsideTempHighNull = value;
                _outsideTempHigh = -1;
            }
        }

        public bool IsOutsideTempLowNull
        {
            get { return _isOutsideTempLowNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isOutsideTempLowNull = value;
                _outsideTempLow = -1;
            }
        }

        public bool IsOutsideTempAverageNull
        {
            get { return _isOutsideTempAverageNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isOutsideTempAverageNull = value;
                _outsideTempAverage = -1;
            }
        }


        #endregion

        #region Constructor
        public Snapshot()
        {
        }
        #endregion

        #region Methods
        public static Snapshot LoadSnapshot(int snapshotId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("LoadSnapshot", ThermostatMonitorLib.Global.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", snapshotId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetSnapshot(row);
        }

        internal static Snapshot GetSnapshot(DataRow row)
        {
            Snapshot result = new Snapshot();
            if (row.Table.Columns.Contains("Id"))
            {
                if (Convert.IsDBNull(row["Id"]))
                {
                    result._isIdNull = true;
                }
                else
                {
                    result._id = Convert.ToInt32(row["Id"]);
                    result._isIdNull = false;
                }
            }

            if (row.Table.Columns.Contains("ThermostatId"))
            {
                if (Convert.IsDBNull(row["ThermostatId"]))
                {
                    result._isThermostatIdNull = true;
                }
                else
                {
                    result._thermostatId = Convert.ToInt32(row["ThermostatId"]);
                    result._isThermostatIdNull = false;
                }
            }

            if (row.Table.Columns.Contains("StartTime"))
            {
                if (Convert.IsDBNull(row["StartTime"]))
                {
                    result._isStartTimeNull = true;
                }
                else
                {
                    result._startTime = Convert.ToDateTime(row["StartTime"]);
                    result._isStartTimeNull = false;
                }
            }

            if (row.Table.Columns.Contains("Seconds"))
            {
                if (Convert.IsDBNull(row["Seconds"]))
                {
                    result._isSecondsNull = true;
                }
                else
                {
                    result._seconds = Convert.ToInt32(row["Seconds"]);
                    result._isSecondsNull = false;
                }
            }

            if (row.Table.Columns.Contains("Mode"))
            {
                if (Convert.IsDBNull(row["Mode"]))
                {
                    result._isModeNull = true;
                }
                else
                {
                    result._mode = Convert.ToString(row["Mode"]);
                    result._isModeNull = false;
                }
            }

            if (row.Table.Columns.Contains("InsideTempHigh"))
            {
                if (Convert.IsDBNull(row["InsideTempHigh"]))
                {
                    result._isInsideTempHighNull = true;
                }
                else
                {
                    result._insideTempHigh = Convert.ToInt32(row["InsideTempHigh"]);
                    result._isInsideTempHighNull = false;
                }
            }

            if (row.Table.Columns.Contains("InsideTempLow"))
            {
                if (Convert.IsDBNull(row["InsideTempLow"]))
                {
                    result._isInsideTempLowNull = true;
                }
                else
                {
                    result._insideTempLow = Convert.ToInt32(row["InsideTempLow"]);
                    result._isInsideTempLowNull = false;
                }
            }

            if (row.Table.Columns.Contains("InsideTempAverage"))
            {
                if (Convert.IsDBNull(row["InsideTempAverage"]))
                {
                    result._isInsideTempAverageNull = true;
                }
                else
                {
                    result._insideTempAverage = Convert.ToInt32(row["InsideTempAverage"]);
                    result._isInsideTempAverageNull = false;
                }
            }

            if (row.Table.Columns.Contains("OutsideTempHigh"))
            {
                if (Convert.IsDBNull(row["OutsideTempHigh"]))
                {
                    result._isOutsideTempHighNull = true;
                }
                else
                {
                    result._outsideTempHigh = Convert.ToInt32(row["OutsideTempHigh"]);
                    result._isOutsideTempHighNull = false;
                }
            }

            if (row.Table.Columns.Contains("OutsideTempLow"))
            {
                if (Convert.IsDBNull(row["OutsideTempLow"]))
                {
                    result._isOutsideTempLowNull = true;
                }
                else
                {
                    result._outsideTempLow = Convert.ToInt32(row["OutsideTempLow"]);
                    result._isOutsideTempLowNull = false;
                }
            }

            if (row.Table.Columns.Contains("OutsideTempAverage"))
            {
                if (Convert.IsDBNull(row["OutsideTempAverage"]))
                {
                    result._isOutsideTempAverageNull = true;
                }
                else
                {
                    result._outsideTempAverage = Convert.ToInt32(row["OutsideTempAverage"]);
                    result._isOutsideTempAverageNull = false;
                }
            }

            return result;
        }

        public static int SaveSnapshot(Snapshot snapshot)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveSnapshot", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (snapshot._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", snapshot._id);
            }

            if (snapshot._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@ThermostatId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ThermostatId", snapshot._thermostatId);
            }

            if (snapshot._isStartTimeNull)
            {
                cmd.Parameters.AddWithValue("@StartTime", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StartTime", snapshot._startTime);
            }

            if (snapshot._isSecondsNull)
            {
                cmd.Parameters.AddWithValue("@Seconds", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Seconds", snapshot._seconds);
            }

            if (snapshot._isModeNull)
            {
                cmd.Parameters.AddWithValue("@Mode", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Mode", snapshot._mode);
            }

            if (snapshot._isInsideTempHighNull)
            {
                cmd.Parameters.AddWithValue("@InsideTempHigh", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@InsideTempHigh", snapshot._insideTempHigh);
            }

            if (snapshot._isInsideTempLowNull)
            {
                cmd.Parameters.AddWithValue("@InsideTempLow", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@InsideTempLow", snapshot._insideTempLow);
            }

            if (snapshot._isInsideTempAverageNull)
            {
                cmd.Parameters.AddWithValue("@InsideTempAverage", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@InsideTempAverage", snapshot._insideTempAverage);
            }

            if (snapshot._isOutsideTempHighNull)
            {
                cmd.Parameters.AddWithValue("@OutsideTempHigh", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OutsideTempHigh", snapshot._outsideTempHigh);
            }

            if (snapshot._isOutsideTempLowNull)
            {
                cmd.Parameters.AddWithValue("@OutsideTempLow", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OutsideTempLow", snapshot._outsideTempLow);
            }

            if (snapshot._isOutsideTempAverageNull)
            {
                cmd.Parameters.AddWithValue("@OutsideTempAverage", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OutsideTempAverage", snapshot._outsideTempAverage);
            }

            cmd.Connection.Open();
            try
            {
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                cmd.Connection.Close();
            }
            snapshot.Id = result;
            return result;
        }

        public static void DeleteSnapshot(int snapshotId)
        {
            SqlCommand cmd = new SqlCommand("DeleteSnapshot", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", snapshotId);
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public object GetPropertyValue(string propertyName)
        {
            return Utils.GetPropertyValue<Snapshot>(this, propertyName);
        }

        #endregion

    }
}




