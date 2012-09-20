


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Temperature
    {
        #region Declarations
        int _id;
        int _thermostatId;
        DateTime _logDate;
        double _degrees;
        int _logPrecision;

        bool _isIdNull = true;
        bool _isThermostatIdNull = true;
        bool _isLogDateNull = true;
        bool _isDegreesNull = true;
        bool _isLogPrecisionNull = true;

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

        public DateTime LogDate
        {
            get { return _logDate; }
            set
            {
                _logDate = value;
                _isLogDateNull = false;
            }
        }

        public double Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                _isDegreesNull = false;
            }
        }

        public int LogPrecision
        {
            get { return _logPrecision; }
            set
            {
                _logPrecision = value;
                _isLogPrecisionNull = false;
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

        public bool IsLogPrecisionNull
        {
            get { return _isLogPrecisionNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isLogPrecisionNull = value;
                _logPrecision = -1;
            }
        }


        #endregion


        #region Constructor
        public Temperature()
        {
        }
        #endregion

        #region Methods
        public static Temperature LoadTemperature(int temperatureId)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("temperatures_load", ThermostatMonitorLib.Global.MySqlConnection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", temperatureId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetTemperature(row);
        }

        internal static Temperature GetTemperature(DataRow row)
        {
            Temperature result = new Temperature();
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

            if (row.Table.Columns.Contains("log_date"))
            {
                if (Convert.IsDBNull(row["log_date"]))
                {
                    result._isLogDateNull = true;
                }
                else
                {
                    result._logDate = Convert.ToDateTime(row["log_date"]);
                    result._isLogDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("degrees"))
            {
                if (Convert.IsDBNull(row["degrees"]))
                {
                    result._isDegreesNull = true;
                }
                else
                {
                    result._degrees = Convert.ToDouble(row["degrees"]);
                    result._isDegreesNull = false;
                }
            }

            if (row.Table.Columns.Contains("log_precision"))
            {
                if (Convert.IsDBNull(row["log_precision"]))
                {
                    result._isLogPrecisionNull = true;
                }
                else
                {
                    result._logPrecision = Convert.ToInt32(row["log_precision"]);
                    result._isLogPrecisionNull = false;
                }
            }

            return result;
        }

        public static int SaveTemperature(Temperature temperature)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("temperatures_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (temperature._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", temperature._id);
            }

            if (temperature._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@thermostat_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@thermostat_id", temperature._thermostatId);
            }

            if (temperature._isLogDateNull)
            {
                cmd.Parameters.AddWithValue("@log_date", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@log_date", temperature._logDate);
            }

            if (temperature._isDegreesNull)
            {
                cmd.Parameters.AddWithValue("@degrees", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@degrees", temperature._degrees);
            }

            if (temperature._isLogPrecisionNull)
            {
                cmd.Parameters.AddWithValue("@log_precision", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@log_precision", temperature._logPrecision);
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
            temperature.Id = result;
            return result;
        }

        public static void DeleteTemperature(int temperatureId)
        {
            MySqlCommand cmd = new MySqlCommand("temperatures_delete", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", temperatureId);
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
            return Utils.GetPropertyValue<Temperature>(this, propertyName);
        }

        #endregion

    }
}


