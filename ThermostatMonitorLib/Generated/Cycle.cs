using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Cycle
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _thermostatId;
        System.String _cycleType;
        System.DateTime _startDate;
        System.DateTime _endDate;
        System.Int16 _startPrecision;
        System.Int16 _endPrecision;

        bool _isIdNull = true;
        bool _isThermostatIdNull = true;
        bool _isCycleTypeNull = true;
        bool _isStartDateNull = true;
        bool _isEndDateNull = true;
        bool _isStartPrecisionNull = true;
        bool _isEndPrecisionNull = true;

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

        public System.String CycleType
        {
            get { return _cycleType; }
            set
            {
                _cycleType = value;
                _isCycleTypeNull = false;
            }
        }

        public System.DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                _isStartDateNull = false;
            }
        }

        public System.DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                _isEndDateNull = false;
            }
        }

        public System.Int16 StartPrecision
        {
            get { return _startPrecision; }
            set
            {
                _startPrecision = value;
                _isStartPrecisionNull = false;
            }
        }

        public System.Int16 EndPrecision
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
            SqlDataAdapter adapter = new SqlDataAdapter("LoadCycle", ThermostatMonitorLib.Global.Connection);
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

            if (row.Table.Columns.Contains("CycleType"))
            {
                if (Convert.IsDBNull(row["CycleType"]))
                {
                    result._isCycleTypeNull = true;
                }
                else
                {
                    result._cycleType = Convert.ToString(row["CycleType"]);
                    result._isCycleTypeNull = false;
                }
            }

            if (row.Table.Columns.Contains("StartDate"))
            {
                if (Convert.IsDBNull(row["StartDate"]))
                {
                    result._isStartDateNull = true;
                }
                else
                {
                    result._startDate = Convert.ToDateTime(row["StartDate"]);
                    result._isStartDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("EndDate"))
            {
                if (Convert.IsDBNull(row["EndDate"]))
                {
                    result._isEndDateNull = true;
                }
                else
                {
                    result._endDate = Convert.ToDateTime(row["EndDate"]);
                    result._isEndDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("StartPrecision"))
            {
                if (Convert.IsDBNull(row["StartPrecision"]))
                {
                    result._isStartPrecisionNull = true;
                }
                else
                {
                    result._startPrecision = Convert.ToInt16(row["StartPrecision"]);
                    result._isStartPrecisionNull = false;
                }
            }

            if (row.Table.Columns.Contains("EndPrecision"))
            {
                if (Convert.IsDBNull(row["EndPrecision"]))
                {
                    result._isEndPrecisionNull = true;
                }
                else
                {
                    result._endPrecision = Convert.ToInt16(row["EndPrecision"]);
                    result._isEndPrecisionNull = false;
                }
            }

            return result;
        }

        public static int SaveCycle(Cycle cycle)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveCycle", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (cycle._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", cycle._id);
            }

            if (cycle._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@ThermostatId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ThermostatId", cycle._thermostatId);
            }

            if (cycle._isCycleTypeNull)
            {
                cmd.Parameters.AddWithValue("@CycleType", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CycleType", cycle._cycleType);
            }

            if (cycle._isStartDateNull)
            {
                cmd.Parameters.AddWithValue("@StartDate", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StartDate", cycle._startDate);
            }

            if (cycle._isEndDateNull)
            {
                cmd.Parameters.AddWithValue("@EndDate", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@EndDate", cycle._endDate);
            }

            if (cycle._isStartPrecisionNull)
            {
                cmd.Parameters.AddWithValue("@StartPrecision", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StartPrecision", cycle._startPrecision);
            }

            if (cycle._isEndPrecisionNull)
            {
                cmd.Parameters.AddWithValue("@EndPrecision", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@EndPrecision", cycle._endPrecision);
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
            SqlCommand cmd = new SqlCommand("DeleteCycle", ThermostatMonitorLib.Global.Connection);
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




