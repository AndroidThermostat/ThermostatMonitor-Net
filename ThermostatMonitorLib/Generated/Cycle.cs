


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Cycle
    {
        #region Declarations
        int _id;
        int _thermostatId;
        string _cycleType;
        DateTime _startDate;
        DateTime _endDate;
        int _startPrecision;
        int _endPrecision;

        bool _isIdNull = true;
        bool _isThermostatIdNull = true;
        bool _isCycleTypeNull = true;
        bool _isStartDateNull = true;
        bool _isEndDateNull = true;
        bool _isStartPrecisionNull = true;
        bool _isEndPrecisionNull = true;

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

        public string CycleType
        {
            get { return _cycleType; }
            set
            {
                _cycleType = value;
                _isCycleTypeNull = false;
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                _isStartDateNull = false;
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                _isEndDateNull = false;
            }
        }

        public int StartPrecision
        {
            get { return _startPrecision; }
            set
            {
                _startPrecision = value;
                _isStartPrecisionNull = false;
            }
        }

        public int EndPrecision
        {
            get { return _endPrecision; }
            set
            {
                _endPrecision = value;
                _isEndPrecisionNull = false;
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

        public bool IsCycleTypeNull
        {
            get { return _isCycleTypeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isCycleTypeNull = value;
                _cycleType = System.String.Empty;
            }
        }

        public bool IsStartDateNull
        {
            get { return _isStartDateNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isStartDateNull = value;
                _startDate = DateTime.MinValue;
            }
        }

        public bool IsEndDateNull
        {
            get { return _isEndDateNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isEndDateNull = value;
                _endDate = DateTime.MinValue;
            }
        }

        public bool IsStartPrecisionNull
        {
            get { return _isStartPrecisionNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isStartPrecisionNull = value;
                _startPrecision = -1;
            }
        }

        public bool IsEndPrecisionNull
        {
            get { return _isEndPrecisionNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isEndPrecisionNull = value;
                _endPrecision = -1;
            }
        }


        #endregion


        #region Constructor
        public Cycle()
        {
        }
        #endregion

        #region Methods
        public static Cycle LoadCycle(int cycleId)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("cycles_load", ThermostatMonitorLib.Global.MySqlConnection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", cycleId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetCycle(row);
        }

        internal static Cycle GetCycle(DataRow row)
        {
            Cycle result = new Cycle();
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

            if (row.Table.Columns.Contains("cycle_type"))
            {
                if (Convert.IsDBNull(row["cycle_type"]))
                {
                    result._isCycleTypeNull = true;
                }
                else
                {
                    result._cycleType = Convert.ToString(row["cycle_type"]);
                    result._isCycleTypeNull = false;
                }
            }

            if (row.Table.Columns.Contains("start_date"))
            {
                if (Convert.IsDBNull(row["start_date"]))
                {
                    result._isStartDateNull = true;
                }
                else
                {
                    result._startDate = Convert.ToDateTime(row["start_date"]);
                    result._isStartDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("end_date"))
            {
                if (Convert.IsDBNull(row["end_date"]))
                {
                    result._isEndDateNull = true;
                }
                else
                {
                    result._endDate = Convert.ToDateTime(row["end_date"]);
                    result._isEndDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("start_precision"))
            {
                if (Convert.IsDBNull(row["start_precision"]))
                {
                    result._isStartPrecisionNull = true;
                }
                else
                {
                    result._startPrecision = Convert.ToInt32(row["start_precision"]);
                    result._isStartPrecisionNull = false;
                }
            }

            if (row.Table.Columns.Contains("end_precision"))
            {
                if (Convert.IsDBNull(row["end_precision"]))
                {
                    result._isEndPrecisionNull = true;
                }
                else
                {
                    result._endPrecision = Convert.ToInt32(row["end_precision"]);
                    result._isEndPrecisionNull = false;
                }
            }

            return result;
        }

        public static int SaveCycle(Cycle cycle)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("cycles_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (cycle._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", cycle._id);
            }

            if (cycle._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@thermostat_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@thermostat_id", cycle._thermostatId);
            }

            if (cycle._isCycleTypeNull)
            {
                cmd.Parameters.AddWithValue("@cycle_type", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@cycle_type", cycle._cycleType);
            }

            if (cycle._isStartDateNull)
            {
                cmd.Parameters.AddWithValue("@start_date", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@start_date", cycle._startDate);
            }

            if (cycle._isEndDateNull)
            {
                cmd.Parameters.AddWithValue("@end_date", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@end_date", cycle._endDate);
            }

            if (cycle._isStartPrecisionNull)
            {
                cmd.Parameters.AddWithValue("@start_precision", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@start_precision", cycle._startPrecision);
            }

            if (cycle._isEndPrecisionNull)
            {
                cmd.Parameters.AddWithValue("@end_precision", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@end_precision", cycle._endPrecision);
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
            cycle.Id = result;
            return result;
        }

        public static void DeleteCycle(int cycleId)
        {
            MySqlCommand cmd = new MySqlCommand("cycles_delete", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", cycleId);
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
            return Utils.GetPropertyValue<Cycle>(this, propertyName);
        }

        #endregion

    }
}


