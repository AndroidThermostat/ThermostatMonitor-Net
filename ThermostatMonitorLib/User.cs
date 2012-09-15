using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    public partial class User
    {
        public static User LoadByAuthCode(string cookie)
        {
            Users users = Users.LoadUsers("SELECT * FROM Users WHERE AuthCode = @AuthCode", CommandType.Text, new SqlParameter[] { new SqlParameter("@AuthCode", cookie) });
            if (users.Count == 0) return null; else return users[0];
        }

        public static User LoadUser(string email)
        {
            Users users=Users.LoadUsers("SELECT * FROM Users WHERE lower(EmailAddress)=@Email",CommandType.Text,new SqlParameter[]{new SqlParameter("@Email",email.ToLower())});
            if (users.Count==0) return null; else return users[0];
        }

        public static User LoadUser(string email, string password)
        {
            string passwordHash = Utils.HashPassword(password);

            Users users = Users.LoadUsers("SELECT * FROM Users WHERE lower(EmailAddress)=@Email AND Password=@Password", CommandType.Text, new SqlParameter[] { new SqlParameter("@Email", email.ToLower()), new SqlParameter("@Password", passwordHash) });
            if (users.Count == 0) return null; else return users[0];
        }


    }
}
