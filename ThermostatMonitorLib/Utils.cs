using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography;

namespace ThermostatMonitorLib
{
    public class Utils
    {
        #region AutoGen Methods
        public static object GetPropertyValue<t>(t obj, string propertyName)
        {
            var type = typeof(t);
            var prop = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            return prop.GetValue(obj, null);
        }

        public static DataTable ExecuteQuery(string sql, System.Data.CommandType commandType, SqlParameter[] parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Global.Connection);
            adapter.SelectCommand.CommandType = commandType;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters) adapter.SelectCommand.Parameters.Add(parameter);
            }
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        #endregion


        public static string HashPassword(string password)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hash = sha.ComputeHash(Encoding.Unicode.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash) sb.Append(String.Format("{0,2:X2}", b));
            return sb.ToString();
        }

        public static string DataTableToCSV(DataTable dt)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i=0;i<dt.Columns.Count;i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append(dt.Columns[i].ColumnName);
            }
            sb.Append(System.Environment.NewLine);


            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sb.Append(",");
                    if (!Convert.IsDBNull(row[i]))
                    {
                        if (dt.Columns[i].DataType == typeof(DateTime))
                        {
                            sb.Append(Convert.ToDateTime(row[i]).ToString("M/d/yyyy H:mm"));
                        }
                        else
                        {
                            sb.Append(row[i].ToString());
                        }
                    }
                }
                sb.Append(System.Environment.NewLine);
            }
            return sb.ToString();
        }

        public static string GetUrlContents(string url)
        {
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string result = sr.ReadToEnd();
            return result;
        }

    }
}
