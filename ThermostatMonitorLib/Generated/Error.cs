


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Error
    {
        #region Declarations
        int _id;
        int _userId;
        DateTime _logDate;
        string _errorMessage;
        string _url;

        bool _isIdNull = true;
        bool _isUserIdNull = true;
        bool _isLogDateNull = true;
        bool _isErrorMessageNull = true;
        bool _isUrlNull = true;

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

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                _isUserIdNull = false;
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

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                _isErrorMessageNull = false;
            }
        }

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                _isUrlNull = false;
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

        public bool IsUserIdNull
        {
            get { return _isUserIdNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isUserIdNull = value;
                _userId = -1;
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

        public bool IsErrorMessageNull
        {
            get { return _isErrorMessageNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isErrorMessageNull = value;
                _errorMessage = System.String.Empty;
            }
        }

        public bool IsUrlNull
        {
            get { return _isUrlNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isUrlNull = value;
                _url = System.String.Empty;
            }
        }


        #endregion


        #region Constructor
        public Error()
        {
        }
        #endregion

        #region Methods
        public static Error LoadError(int errorId)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("errors_load", ThermostatMonitorLib.Global.MySqlConnection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", errorId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetError(row);
        }

        internal static Error GetError(DataRow row)
        {
            Error result = new Error();
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

            if (row.Table.Columns.Contains("user_id"))
            {
                if (Convert.IsDBNull(row["user_id"]))
                {
                    result._isUserIdNull = true;
                }
                else
                {
                    result._userId = Convert.ToInt32(row["user_id"]);
                    result._isUserIdNull = false;
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

            if (row.Table.Columns.Contains("error_message"))
            {
                if (Convert.IsDBNull(row["error_message"]))
                {
                    result._isErrorMessageNull = true;
                }
                else
                {
                    result._errorMessage = Convert.ToString(row["error_message"]);
                    result._isErrorMessageNull = false;
                }
            }

            if (row.Table.Columns.Contains("url"))
            {
                if (Convert.IsDBNull(row["url"]))
                {
                    result._isUrlNull = true;
                }
                else
                {
                    result._url = Convert.ToString(row["url"]);
                    result._isUrlNull = false;
                }
            }

            return result;
        }

        public static int SaveError(Error error)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("errors_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (error._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", error._id);
            }

            if (error._isUserIdNull)
            {
                cmd.Parameters.AddWithValue("@user_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@user_id", error._userId);
            }

            if (error._isLogDateNull)
            {
                cmd.Parameters.AddWithValue("@log_date", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@log_date", error._logDate);
            }

            if (error._isErrorMessageNull)
            {
                cmd.Parameters.AddWithValue("@error_message", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@error_message", error._errorMessage);
            }

            if (error._isUrlNull)
            {
                cmd.Parameters.AddWithValue("@url", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@url", error._url);
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
            error.Id = result;
            return result;
        }

        public static void DeleteError(int errorId)
        {
            MySqlCommand cmd = new MySqlCommand("errors_delete", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", errorId);
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
            return Utils.GetPropertyValue<Error>(this, propertyName);
        }

        #endregion

    }
}


