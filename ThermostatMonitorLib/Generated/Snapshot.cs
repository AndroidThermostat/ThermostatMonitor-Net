


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Snapshot
    {
        #region Declarations
        int _id;
        int _thermostatId;
        DateTime _startTime;
        int _seconds;
        string _mode;
        int _insideTempHigh;
        int _insideTempLow;
        int _insideTempAverage;
        int _outsideTempHigh;
        int _outsideTempLow;
        int _outsideTempAverage;

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
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                _isIdNull = false;
            }
        }

        public int ThermostatId
        {
            get { return _thermostatId; }
            set
            {
                _thermostatId = value;
                _isThermostatIdNull = false;
            }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                _isStartTimeNull = false;
            }
        }

        public int Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                _isSecondsNull = false;
            }
        }

        public string Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                _isModeNull = false;
            }
        }

        public int InsideTempHigh
        {
            get { return _insideTempHigh; }
            set
            {
                _insideTempHigh = value;
                _isInsideTempHighNull = false;
            }
        }

        public int InsideTempLow
        {
            get { return _insideTempLow; }
            set
            {
                _insideTempLow = value;
                _isInsideTempLowNull = false;
            }
        }

        public int InsideTempAverage
        {
            get { return _insideTempAverage; }
            set
            {
                _insideTempAverage = value;
                _isInsideTempAverageNull = false;
            }
        }

        public int OutsideTempHigh
        {
            get { return _outsideTempHigh; }
            set
            {
                _outsideTempHigh = value;
                _isOutsideTempHighNull = false;
            }
        }

        public int OutsideTempLow
        {
            get { return _outsideTempLow; }
            set
            {
                _outsideTempLow = value;
                _isOutsideTempLowNull = false;
            }
        }

        public int OutsideTempAverage
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
            MySqlDataAdapter adapter = new MySqlDataAdapter("snapshots_load", ThermostatMonitorLib.Global.MySqlConnection);
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
            if (row.Table.Columns.Contains("id"))
            {
                if (Convert.IsDBNull(row["id"]))
                {
                    result._isIdNull = true;
                }
                else
                {
                    result._id = Convert.ToInt32(row["id"]);
                    result._isIdNull = false;
                }
            }

            if (row.Table.Columns.Contains("thermostat_id"))
            {
                if (Convert.IsDBNull(row["thermostat_id"]))
                {
                    result._isThermostatIdNull = true;
                }
                else
                {
                    result._thermostatId = Convert.ToInt32(row["thermostat_id"]);
                    result._isThermostatIdNull = false;
                }
            }

            if (row.Table.Columns.Contains("start_time"))
            {
                if (Convert.IsDBNull(row["start_time"]))
                {
                    result._isStartTimeNull = true;
                }
                else
                {
                    result._startTime = Convert.ToDateTime(row["start_time"]);
                    result._isStartTimeNull = false;
                }
            }

            if (row.Table.Columns.Contains("seconds"))
            {
                if (Convert.IsDBNull(row["seconds"]))
                {
                    result._isSecondsNull = true;
                }
                else
                {
                    result._seconds = Convert.ToInt32(row["seconds"]);
                    result._isSecondsNull = false;
                }
            }

            if (row.Table.Columns.Contains("mode"))
            {
                if (Convert.IsDBNull(row["mode"]))
                {
                    result._isModeNull = true;
                }
                else
                {
                    result._mode = Convert.ToString(row["mode"]);
                    result._isModeNull = false;
                }
            }

            if (row.Table.Columns.Contains("inside_temp_high"))
            {
                if (Convert.IsDBNull(row["inside_temp_high"]))
                {
                    result._isInsideTempHighNull = true;
                }
                else
                {
                    result._insideTempHigh = Convert.ToInt32(row["inside_temp_high"]);
                    result._isInsideTempHighNull = false;
                }
            }

            if (row.Table.Columns.Contains("inside_temp_low"))
            {
                if (Convert.IsDBNull(row["inside_temp_low"]))
                {
                    result._isInsideTempLowNull = true;
                }
                else
                {
                    result._insideTempLow = Convert.ToInt32(row["inside_temp_low"]);
                    result._isInsideTempLowNull = false;
                }
            }

            if (row.Table.Columns.Contains("inside_temp_average"))
            {
                if (Convert.IsDBNull(row["inside_temp_average"]))
                {
                    result._isInsideTempAverageNull = true;
                }
                else
                {
                    result._insideTempAverage = Convert.ToInt32(row["inside_temp_average"]);
                    result._isInsideTempAverageNull = false;
                }
            }

            if (row.Table.Columns.Contains("outside_temp_high"))
            {
                if (Convert.IsDBNull(row["outside_temp_high"]))
                {
                    result._isOutsideTempHighNull = true;
                }
                else
                {
                    result._outsideTempHigh = Convert.ToInt32(row["outside_temp_high"]);
                    result._isOutsideTempHighNull = false;
                }
            }

            if (row.Table.Columns.Contains("outside_temp_low"))
            {
                if (Convert.IsDBNull(row["outside_temp_low"]))
                {
                    result._isOutsideTempLowNull = true;
                }
                else
                {
                    result._outsideTempLow = Convert.ToInt32(row["outside_temp_low"]);
                    result._isOutsideTempLowNull = false;
                }
            }

            if (row.Table.Columns.Contains("outside_temp_average"))
            {
                if (Convert.IsDBNull(row["outside_temp_average"]))
                {
                    result._isOutsideTempAverageNull = true;
                }
                else
                {
                    result._outsideTempAverage = Convert.ToInt32(row["outside_temp_average"]);
                    result._isOutsideTempAverageNull = false;
                }
            }

            return result;
        }

        public static int SaveSnapshot(Snapshot snapshot)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("snapshots_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (snapshot._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", snapshot._id);
            }

            if (snapshot._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@thermostat_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@thermostat_id", snapshot._thermostatId);
            }

            if (snapshot._isStartTimeNull)
            {
                cmd.Parameters.AddWithValue("@start_time", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@start_time", snapshot._startTime);
            }

            if (snapshot._isSecondsNull)
            {
                cmd.Parameters.AddWithValue("@seconds", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@seconds", snapshot._seconds);
            }

            if (snapshot._isModeNull)
            {
                cmd.Parameters.AddWithValue("@mode", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@mode", snapshot._mode);
            }

            if (snapshot._isInsideTempHighNull)
            {
                cmd.Parameters.AddWithValue("@inside_temp_high", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@inside_temp_high", snapshot._insideTempHigh);
            }

            if (snapshot._isInsideTempLowNull)
            {
                cmd.Parameters.AddWithValue("@inside_temp_low", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@inside_temp_low", snapshot._insideTempLow);
            }

            if (snapshot._isInsideTempAverageNull)
            {
                cmd.Parameters.AddWithValue("@inside_temp_average", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@inside_temp_average", snapshot._insideTempAverage);
            }

            if (snapshot._isOutsideTempHighNull)
            {
                cmd.Parameters.AddWithValue("@outside_temp_high", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@outside_temp_high", snapshot._outsideTempHigh);
            }

            if (snapshot._isOutsideTempLowNull)
            {
                cmd.Parameters.AddWithValue("@outside_temp_low", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@outside_temp_low", snapshot._outsideTempLow);
            }

            if (snapshot._isOutsideTempAverageNull)
            {
                cmd.Parameters.AddWithValue("@outside_temp_average", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@outside_temp_average", snapshot._outsideTempAverage);
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
            MySqlCommand cmd = new MySqlCommand("snapshots_delete", ThermostatMonitorLib.Global.MySqlConnection);
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


