


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class User
    {
        #region Declarations
        int _id;
        string _emailAddress;
        string _password;
        string _authCode;

        bool _isIdNull = true;
        bool _isEmailAddressNull = true;
        bool _isPasswordNull = true;
        bool _isAuthCodeNull = true;

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

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                _isEmailAddressNull = false;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                _isPasswordNull = false;
            }
        }

        public string AuthCode
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
            MySqlDataAdapter adapter = new MySqlDataAdapter("users_load", ThermostatMonitorLib.Global.MySqlConnection);
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

            if (row.Table.Columns.Contains("email_address"))
            {
                if (Convert.IsDBNull(row["email_address"]))
                {
                    result._isEmailAddressNull = true;
                }
                else
                {
                    result._emailAddress = Convert.ToString(row["email_address"]);
                    result._isEmailAddressNull = false;
                }
            }

            if (row.Table.Columns.Contains("password"))
            {
                if (Convert.IsDBNull(row["password"]))
                {
                    result._isPasswordNull = true;
                }
                else
                {
                    result._password = Convert.ToString(row["password"]);
                    result._isPasswordNull = false;
                }
            }

            if (row.Table.Columns.Contains("auth_code"))
            {
                if (Convert.IsDBNull(row["auth_code"]))
                {
                    result._isAuthCodeNull = true;
                }
                else
                {
                    result._authCode = Convert.ToString(row["auth_code"]);
                    result._isAuthCodeNull = false;
                }
            }

            return result;
        }

        public static int SaveUser(User user)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("users_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (user._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", user._id);
            }

            if (user._isEmailAddressNull)
            {
                cmd.Parameters.AddWithValue("@email_address", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@email_address", user._emailAddress);
            }

            if (user._isPasswordNull)
            {
                cmd.Parameters.AddWithValue("@password", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@password", user._password);
            }

            if (user._isAuthCodeNull)
            {
                cmd.Parameters.AddWithValue("@auth_code", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@auth_code", user._authCode);
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
            MySqlCommand cmd = new MySqlCommand("users_delete", ThermostatMonitorLib.Global.MySqlConnection);
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


