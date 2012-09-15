using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class OutsideCondition
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _degrees;
        System.DateTime _logDate;
        System.Int32 _locationId;

        bool _isIdNull = true;
        bool _isDegreesNull = true;
        bool _isLogDateNull = true;
        bool _isLocationIdNull = true;

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

        public System.Int32 Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                _isDegreesNull = false;
            }
        }

        public System.DateTime LogDate
        {
            get { return _logDate; }
            set
            {
                _logDate = value;
                _isLogDateNull = false;
            }
        }

        public System.Int32 LocationId
        {
            get { return _locationId; }
            set
            {
                _locationId = value;
                _isLocationIdNull = false;
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

        public bool IsDegreesNull
        {
            get { return _isDegreesNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isDegreesNull = value;
                _degrees = -1;
            }
        }

        public bool IsLogDateNull
        {
            get { return _isLogDateNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isLogDateNull = value;
                _logDate = DateTime.MinValue;
            }
        }

        public bool IsLocationIdNull
        {
            get { return _isLocationIdNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isLocationIdNull = value;
                _locationId = -1;
            }
        }


        #endregion

        #region Constructor
        public OutsideCondition()
        {
        }
        #endregion

        #region Methods
        public static OutsideCondition LoadOutsideCondition(int outsideConditionId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("LoadOutsideCondition", ThermostatMonitorLib.Global.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", outsideConditionId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetOutsideCondition(row);
        }

        internal static OutsideCondition GetOutsideCondition(DataRow row)
        {
            OutsideCondition result = new OutsideCondition();
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

            if (row.Table.Columns.Contains("Degrees"))
            {
                if (Convert.IsDBNull(row["Degrees"]))
                {
                    result._isDegreesNull = true;
                }
                else
                {
                    result._degrees = Convert.ToInt32(row["Degrees"]);
                    result._isDegreesNull = false;
                }
            }

            if (row.Table.Columns.Contains("LogDate"))
            {
                if (Convert.IsDBNull(row["LogDate"]))
                {
                    result._isLogDateNull = true;
                }
                else
                {
                    result._logDate = Convert.ToDateTime(row["LogDate"]);
                    result._isLogDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("LocationId"))
            {
                if (Convert.IsDBNull(row["LocationId"]))
                {
                    result._isLocationIdNull = true;
                }
                else
                {
                    result._locationId = Convert.ToInt32(row["LocationId"]);
                    result._isLocationIdNull = false;
                }
            }

            return result;
        }

        public static int SaveOutsideCondition(OutsideCondition outsideCondition)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveOutsideCondition", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (outsideCondition._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", outsideCondition._id);
            }

            if (outsideCondition._isDegreesNull)
            {
                cmd.Parameters.AddWithValue("@Degrees", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Degrees", outsideCondition._degrees);
            }

            if (outsideCondition._isLogDateNull)
            {
                cmd.Parameters.AddWithValue("@LogDate", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LogDate", outsideCondition._logDate);
            }

            if (outsideCondition._isLocationIdNull)
            {
                cmd.Parameters.AddWithValue("@LocationId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationId", outsideCondition._locationId);
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
            outsideCondition.Id = result;
            return result;
        }

        public static void DeleteOutsideCondition(int outsideConditionId)
        {
            SqlCommand cmd = new SqlCommand("DeleteOutsideCondition", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", outsideConditionId);
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
            return Utils.GetPropertyValue<OutsideCondition>(this, propertyName);
        }

        #endregion

    }
}




