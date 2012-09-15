using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class UserSettings : System.Collections.Generic.List<UserSetting>
    {

        #region Constructor
        public UserSettings()
        {
        }
        #endregion

        #region Methods
        public static UserSettings LoadUserSettings(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return UserSettings.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static UserSettings ConvertFromDT(DataTable dt)
        {
            UserSettings result = new UserSettings();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(UserSetting.GetUserSetting(row));
            }
            return result;
        }

        public static UserSettings LoadAllUserSettings()
        {
            return UserSettings.LoadUserSettings("LoadUserSettingsAll", CommandType.StoredProcedure, null);
        }

        public UserSetting GetUserSettingById(int userSettingId)
        {
            foreach (UserSetting userSetting in this)
            {
                if (userSetting.Id == userSettingId) return userSetting;
            }
            return null;
        }

        public static UserSettings LoadUserSettingsByUserId(System.Int32 userId)
        {
            return UserSettings.LoadUserSettings("LoadUserSettingsByUserId", CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@UserId", userId) });
        }


        public UserSettings Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            UserSettings result = new UserSettings();
            foreach (var i in sortedList) { result.Add((UserSetting)i); }
            return result;
        }

        #endregion


    }
}

