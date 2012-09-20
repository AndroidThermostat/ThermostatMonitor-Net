using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    public partial class User
    {
        public static User LoadByAuthCode(string cookie)
        {
            Users users = Users.LoadUsers("SELECT * FROM users WHERE auth_code = @AuthCode", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@AuthCode", cookie) });
            if (users.Count == 0) return null; else return users[0];
        }

        public static User LoadUser(string email)
        {
            Users users = Users.LoadUsers("SELECT * FROM users WHERE lower(email_address)=@Email", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@Email", email.ToLower()) });
            if (users.Count==0) return null; else return users[0];
        }

        public static User LoadUser(string email, string password)
        {
            string passwordHash = Utils.HashPassword(password);

            Users users = Users.LoadUsers("SELECT * FROM users WHERE lower(email_address)=@Email AND password=@Password", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@Email", email.ToLower()), new MySqlParameter("@Password", passwordHash) });
            if (users.Count == 0) return null; else return users[0];
        }


    }
}
