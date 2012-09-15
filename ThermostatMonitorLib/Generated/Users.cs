using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Users : System.Collections.Generic.List<User>
    {

        #region Constructor
        public Users()
        {
        }
        #endregion

        #region Methods
        public static Users LoadUsers(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return Users.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static Users ConvertFromDT(DataTable dt)
        {
            Users result = new Users();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(User.GetUser(row));
            }
            return result;
        }

        public static Users LoadAllUsers()
        {
            return Users.LoadUsers("LoadUsersAll", CommandType.StoredProcedure, null);
        }

        public User GetUserById(int userId)
        {
            foreach (User user in this)
            {
                if (user.Id == userId) return user;
            }
            return null;
        }


        public Users Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            Users result = new Users();
            foreach (var i in sortedList) { result.Add((User)i); }
            return result;
        }

        #endregion


    }
}

