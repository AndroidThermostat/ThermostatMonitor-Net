using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Errors : System.Collections.Generic.List<Error>
    {

        #region Constructor
        public Errors()
        {
        }
        #endregion

        #region Methods
        public static Errors LoadErrors(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return Errors.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static Errors ConvertFromDT(DataTable dt)
        {
            Errors result = new Errors();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(Error.GetError(row));
            }
            return result;
        }

        public static Errors LoadAllErrors()
        {
            return Errors.LoadErrors("LoadErrorsAll", CommandType.StoredProcedure, null);
        }

        public Error GetErrorById(int errorId)
        {
            foreach (Error error in this)
            {
                if (error.Id == errorId) return error;
            }
            return null;
        }

        public static Errors LoadErrorsByUserId(System.Int32 userId)
        {
            return Errors.LoadErrors("LoadErrorsByUserId", CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@UserId", userId) });
        }


        public Errors Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            Errors result = new Errors();
            foreach (var i in sortedList) { result.Add((Error)i); }
            return result;
        }

        #endregion


    }
}

