using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Error
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _userId;
        System.DateTime _logDate;
        System.String _errorMessage;
        System.String _url;

        bool _isIdNull = true;
        bool _isUserIdNull = true;
        bool _isLogDateNull = true;
        bool _isErrorMessageNull = true;
        bool _isUrlNull = true;

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

        public System.Int32 UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                _isUserIdNull = false;
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

        public System.String ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                _isErrorMessageNull = false;
            }
        }

        public System.String Url
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
            SqlDataAdapter adapter = new SqlDataAdapter("LoadError", ThermostatMonitorLib.Global.Connection);
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

            if (row.Table.Columns.Contains("UserId"))
            {
                if (Convert.IsDBNull(row["UserId"]))
                {
                    result._isUserIdNull = true;
                }
                else
                {
                    result._userId = Convert.ToInt32(row["UserId"]);
                    result._isUserIdNull = false;
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

            if (row.Table.Columns.Contains("ErrorMessage"))
            {
                if (Convert.IsDBNull(row["ErrorMessage"]))
                {
                    result._isErrorMessageNull = true;
                }
                else
                {
                    result._errorMessage = Convert.ToString(row["ErrorMessage"]);
                    result._isErrorMessageNull = false;
                }
            }

            if (row.Table.Columns.Contains("Url"))
            {
                if (Convert.IsDBNull(row["Url"]))
                {
                    result._isUrlNull = true;
                }
                else
                {
                    result._url = Convert.ToString(row["Url"]);
                    result._isUrlNull = false;
                }
            }

            return result;
        }

        public static int SaveError(Error error)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveError", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (error._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", error._id);
            }

            if (error._isUserIdNull)
            {
                cmd.Parameters.AddWithValue("@UserId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@UserId", error._userId);
            }

            if (error._isLogDateNull)
            {
                cmd.Parameters.AddWithValue("@LogDate", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LogDate", error._logDate);
            }

            if (error._isErrorMessageNull)
            {
                cmd.Parameters.AddWithValue("@ErrorMessage", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ErrorMessage", error._errorMessage);
            }

            if (error._isUrlNull)
            {
                cmd.Parameters.AddWithValue("@Url", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Url", error._url);
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
            SqlCommand cmd = new SqlCommand("DeleteError", ThermostatMonitorLib.Global.Connection);
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




