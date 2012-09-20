using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace ThermostatMonitorLib
{
    public class Global
    {

        
        private static string _connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public static SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }



        private static string _mySqlConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

        public static string MySqlConnectionString
        {
            get { return _mySqlConnectionString; }
            set { _mySqlConnectionString = value; }
        }


        

        public static MySqlConnection MySqlConnection
        {
            get { return new MySqlConnection(_mySqlConnectionString); }
        }
    }
}
