


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class OutsideCondition
    {
        #region Declarations
        int _id;
        int _degrees;
        DateTime _logDate;
        int _locationId;

        bool _isIdNull = true;
        bool _isDegreesNull = true;
        bool _isLogDateNull = true;
        bool _isLocationIdNull = true;

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

        public int Degrees
        {
            get { return _degrees; }
            set
            {
                _degrees = value;
                _isDegreesNull = false;
            }
        }

        public DateTime LogDate
        {
            get { return _logDate; }
            set
            {
                _logDate = value;
                _isLogDateNull = false;
            }
        }

        public int LocationId
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
            MySqlDataAdapter adapter = new MySqlDataAdapter("outside_conditions_load", ThermostatMonitorLib.Global.MySqlConnection);
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

            if (row.Table.Columns.Contains("degrees"))
            {
                if (Convert.IsDBNull(row["degrees"]))
                {
                    result._isDegreesNull = true;
                }
                else
                {
                    result._degrees = Convert.ToInt32(row["degrees"]);
                    result._isDegreesNull = false;
                }
            }

            if (row.Table.Columns.Contains("log_date"))
            {
                if (Convert.IsDBNull(row["log_date"]))
                {
                    result._isLogDateNull = true;
                }
                else
                {
                    result._logDate = Convert.ToDateTime(row["log_date"]);
                    result._isLogDateNull = false;
                }
            }

            if (row.Table.Columns.Contains("location_id"))
            {
                if (Convert.IsDBNull(row["location_id"]))
                {
                    result._isLocationIdNull = true;
                }
                else
                {
                    result._locationId = Convert.ToInt32(row["location_id"]);
                    result._isLocationIdNull = false;
                }
            }

            return result;
        }

        public static int SaveOutsideCondition(OutsideCondition outsideCondition)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("outside_conditions_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (outsideCondition._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", outsideCondition._id);
            }

            if (outsideCondition._isDegreesNull)
            {
                cmd.Parameters.AddWithValue("@degrees", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@degrees", outsideCondition._degrees);
            }

            if (outsideCondition._isLogDateNull)
            {
                cmd.Parameters.AddWithValue("@log_date", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@log_date", outsideCondition._logDate);
            }

            if (outsideCondition._isLocationIdNull)
            {
                cmd.Parameters.AddWithValue("@location_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@location_id", outsideCondition._locationId);
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
            MySqlCommand cmd = new MySqlCommand("outside_conditions_delete", ThermostatMonitorLib.Global.MySqlConnection);
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


