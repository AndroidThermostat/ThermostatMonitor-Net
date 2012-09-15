using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class User
    {
        #region Declarations
        System.Int32 _id;
        System.String _emailAddress;
        System.String _password;
        System.String _authCode;

        bool _isIdNull = true;
        bool _isEmailAddressNull = true;
        bool _isPasswordNull = true;
        bool _isAuthCodeNull = true;

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

        public System.String EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                _isEmailAddressNull = false;
            }
        }

        public System.String Password
        {
            get { return _password; }
            set
            {
                _password = value;
                _isPasswordNull = false;
            }
        }

        public System.String AuthCode
        {
            get { return _authCode; }
            set
            {
                _authCode = value;
                _isAuthCodeNull = false;
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

        public bool IsEmailAddressNull
        {
            get { return _isEmailAddressNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isEmailAddressNull = value;
                _emailAddress = System.String.Empty;
            }
        }

        public bool IsPasswordNull
        {
            get { return _isPasswordNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isPasswordNull = value;
                _password = System.String.Empty;
            }
        }

        public bool IsAuthCodeNull
        {
            get { return _isAuthCodeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isAuthCodeNull = value;
                _authCode = System.String.Empty;
            }
        }


        #endregion

        #region Constructor
        public User()
        {
        }
        #endregion

        #region Methods
        public static User LoadUser(int userId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("LoadUser", ThermostatMonitorLib.Global.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", userId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetUser(row);
        }

        internal static User GetUser(DataRow row)
        {
            User result = new User();
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

            if (row.Table.Columns.Contains("EmailAddress"))
            {
                if (Convert.IsDBNull(row["EmailAddress"]))
                {
                    result._isEmailAddressNull = true;
                }
                else
                {
                    result._emailAddress = Convert.ToString(row["EmailAddress"]);
                    result._isEmailAddressNull = false;
                }
            }

            if (row.Table.Columns.Contains("Password"))
            {
                if (Convert.IsDBNull(row["Password"]))
                {
                    result._isPasswordNull = true;
                }
                else
                {
                    result._password = Convert.ToString(row["Password"]);
                    result._isPasswordNull = false;
                }
            }

            if (row.Table.Columns.Contains("AuthCode"))
            {
                if (Convert.IsDBNull(row["AuthCode"]))
                {
                    result._isAuthCodeNull = true;
                }
                else
                {
                    result._authCode = Convert.ToString(row["AuthCode"]);
                    result._isAuthCodeNull = false;
                }
            }

            return result;
        }

        public static int SaveUser(User user)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveUser", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (user._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", user._id);
            }

            if (user._isEmailAddressNull)
            {
                cmd.Parameters.AddWithValue("@EmailAddress", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@EmailAddress", user._emailAddress);
            }

            if (user._isPasswordNull)
            {
                cmd.Parameters.AddWithValue("@Password", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Password", user._password);
            }

            if (user._isAuthCodeNull)
            {
                cmd.Parameters.AddWithValue("@AuthCode", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AuthCode", user._authCode);
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
            user.Id = result;
            return result;
        }

        public static void DeleteUser(int userId)
        {
            SqlCommand cmd = new SqlCommand("DeleteUser", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", userId);
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
            return Utils.GetPropertyValue<User>(this, propertyName);
        }

        #endregion

    }
}




