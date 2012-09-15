using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Temperature
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _thermostatId;
        System.DateTime _logDate;
        System.Double _degrees;
        System.Int16 _precision;

        bool _isIdNull = true;
        bool _isThermostatIdNull = true;
        bool _isLogDateNull = true;
        bool _isDegreesNull = true;
        bool _isPrecisionNull = true;

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

        public System.Int16 Precision
        {
            get { return _precision; }
            set
            {
                _precision = value;
                _isPrecisionNull = false;
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

        public bool IsPrecisionNull
        {
            get { return _isPrecisionNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isPrecisionNull = value;
                _precision = -1;
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
            SqlDataAdapter adapter = new SqlDataAdapter("LoadTemperature", ThermostatMonitorLib.Global.Connection);
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

            if (row.Table.Columns.Contains("Precision"))
            {
                if (Convert.IsDBNull(row["Precision"]))
                {
                    result._isPrecisionNull = true;
                }
                else
                {
                    result._precision = Convert.ToInt16(row["Precision"]);
                    result._isPrecisionNull = false;
                }
            }

            return result;
        }

        public static int SaveTemperature(Temperature temperature)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveTemperature", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (temperature._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", temperature._id);
            }

            if (temperature._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@ThermostatId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ThermostatId", temperature._thermostatId);
            }

            if (temperature._isLogDateNull)
            {
                cmd.Parameters.AddWithValue("@LogDate", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LogDate", temperature._logDate);
            }

            if (temperature._isDegreesNull)
            {
                cmd.Parameters.AddWithValue("@Degrees", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Degrees", temperature._degrees);
            }

            if (temperature._isPrecisionNull)
            {
                cmd.Parameters.AddWithValue("@Precision", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Precision", temperature._precision);
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
            SqlCommand cmd = new SqlCommand("DeleteTemperature", ThermostatMonitorLib.Global.Connection);
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




