using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class ThermostatSetting
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _thermostatId;
        System.DateTime _logDate;
        System.Double _degrees;
        System.String _mode;

        bool _isIdNull = true;
        bool _isThermostatIdNull = true;
        bool _isLogDateNull = true;
        bool _isDegreesNull = true;
        bool _isModeNull = true;

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

        public System.DateTime LogDate
        {
            get { return _logDate; }
            set
            {
                _logDate = value;
                _isLogDateNull = false;
            }
        }

        public System.Double Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                _isDegreesNull = false;
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

        public bool IsLogDateNull
        {
            get { return _isLogDateNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isLogDateNull = value;
                _logDate = DateTime.MinValue;
            }
        }

        public bool IsDegreesNull
        {
            get { return _isDegreesNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isDegreesNull = value;
                _degrees = -1;
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


        #endregion

        #region Constructor
        public ThermostatSetting()
        {
        }
        #endregion

        #region Methods
        public static ThermostatSetting LoadThermostatSetting(int thermostatSettingId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("LoadThermostatSetting", ThermostatMonitorLib.Global.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", thermostatSettingId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetThermostatSetting(row);
        }

        internal static ThermostatSetting GetThermostatSetting(DataRow row)
        {
            ThermostatSetting result = new ThermostatSetting();
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

            if (row.Table.Columns.Contains("LogDate"))
            {
                if (Convert.IsDBNull(row["LogDate"]))
                {
                    result._isLogDateNull = true;
                }
                else
                {
                    result._logDate = Convert.ToDateTime(row["LogDate"]);
                    result._isLogDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("Degrees"))
            {
                if (Convert.IsDBNull(row["Degrees"]))
                {
                    result._isDegreesNull = true;
                }
                else
                {
                    result._degrees = Convert.ToDouble(row["Degrees"]);
                    result._isDegreesNull = false;
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

            return result;
        }

        public static int SaveThermostatSetting(ThermostatSetting thermostatSetting)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveThermostatSetting", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (thermostatSetting._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", thermostatSetting._id);
            }

            if (thermostatSetting._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@ThermostatId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ThermostatId", thermostatSetting._thermostatId);
            }

            if (thermostatSetting._isLogDateNull)
            {
                cmd.Parameters.AddWithValue("@LogDate", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LogDate", thermostatSetting._logDate);
            }

            if (thermostatSetting._isDegreesNull)
            {
                cmd.Parameters.AddWithValue("@Degrees", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Degrees", thermostatSetting._degrees);
            }

            if (thermostatSetting._isModeNull)
            {
                cmd.Parameters.AddWithValue("@Mode", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Mode", thermostatSetting._mode);
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
            thermostatSetting.Id = result;
            return result;
        }

        public static void DeleteThermostatSetting(int thermostatSettingId)
        {
            SqlCommand cmd = new SqlCommand("DeleteThermostatSetting", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", thermostatSettingId);
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
            return Utils.GetPropertyValue<ThermostatSetting>(this, propertyName);
        }

        #endregion

    }
}




