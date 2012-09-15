using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class UserSetting
    {
        #region Declarations
        System.Int32 _id;
        System.String _zipCode;
        System.DateTime _filterChangeDate;
        System.Int32 _userId;

        bool _isIdNull = true;
        bool _isZipCodeNull = true;
        bool _isFilterChangeDateNull = true;
        bool _isUserIdNull = true;

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

        public System.String ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
                _isZipCodeNull = false;
            }
        }

        public System.DateTime FilterChangeDate
        {
            get { return _filterChangeDate; }
            set
            {
                _filterChangeDate = value;
                _isFilterChangeDateNull = false;
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

        public bool IsZipCodeNull
        {
            get { return _isZipCodeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isZipCodeNull = value;
                _zipCode = System.String.Empty;
            }
        }

        public bool IsFilterChangeDateNull
        {
            get { return _isFilterChangeDateNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isFilterChangeDateNull = value;
                _filterChangeDate = DateTime.MinValue;
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


        #endregion

        #region Constructor
        public UserSetting()
        {
        }
        #endregion

        #region Methods
        public static UserSetting LoadUserSetting(int userSettingId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("LoadUserSetting", ThermostatMonitorLib.Global.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", userSettingId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetUserSetting(row);
        }

        internal static UserSetting GetUserSetting(DataRow row)
        {
            UserSetting result = new UserSetting();
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

            if (row.Table.Columns.Contains("ZipCode"))
            {
                if (Convert.IsDBNull(row["ZipCode"]))
                {
                    result._isZipCodeNull = true;
                }
                else
                {
                    result._zipCode = Convert.ToString(row["ZipCode"]);
                    result._isZipCodeNull = false;
                }
            }

            if (row.Table.Columns.Contains("FilterChangeDate"))
            {
                if (Convert.IsDBNull(row["FilterChangeDate"]))
                {
                    result._isFilterChangeDateNull = true;
                }
                else
                {
                    result._filterChangeDate = Convert.ToDateTime(row["FilterChangeDate"]);
                    result._isFilterChangeDateNull = false;
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

            return result;
        }

        public static int SaveUserSetting(UserSetting userSetting)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveUserSetting", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (userSetting._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", userSetting._id);
            }

            if (userSetting._isZipCodeNull)
            {
                cmd.Parameters.AddWithValue("@ZipCode", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ZipCode", userSetting._zipCode);
            }

            if (userSetting._isFilterChangeDateNull)
            {
                cmd.Parameters.AddWithValue("@FilterChangeDate", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FilterChangeDate", userSetting._filterChangeDate);
            }

            if (userSetting._isUserIdNull)
            {
                cmd.Parameters.AddWithValue("@UserId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@UserId", userSetting._userId);
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
            userSetting.Id = result;
            return result;
        }

        public static void DeleteUserSetting(int userSettingId)
        {
            SqlCommand cmd = new SqlCommand("DeleteUserSetting", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", userSettingId);
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
            return Utils.GetPropertyValue<UserSetting>(this, propertyName);
        }

        #endregion

    }
}




