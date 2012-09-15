using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class TransitionStat
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _thermostatId;
        System.String _transitionType;
        System.Int32 _temperatureDelta;
        System.Double _minutesPerDegree;
        System.Int32 _occurances;

        bool _isIdNull = true;
        bool _isThermostatIdNull = true;
        bool _isTransitionTypeNull = true;
        bool _isTemperatureDeltaNull = true;
        bool _isMinutesPerDegreeNull = true;
        bool _isOccurancesNull = true;

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

        public System.Int32 ThermostatId
        {
            get { return _thermostatId; }
            set
            {
                _thermostatId = value;
                _isThermostatIdNull = false;
            }
        }

        public System.String TransitionType
        {
            get { return _transitionType; }
            set
            {
                _transitionType = value;
                _isTransitionTypeNull = false;
            }
        }

        public System.Int32 TemperatureDelta
        {
            get { return _temperatureDelta; }
            set
            {
                _temperatureDelta = value;
                _isTemperatureDeltaNull = false;
            }
        }

        public System.Double MinutesPerDegree
        {
            get { return _minutesPerDegree; }
            set
            {
                _minutesPerDegree = value;
                _isMinutesPerDegreeNull = false;
            }
        }

        public System.Int32 Occurances
        {
            get { return _occurances; }
            set
            {
                _occurances = value;
                _isOccurancesNull = false;
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

        public bool IsThermostatIdNull
        {
            get { return _isThermostatIdNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isThermostatIdNull = value;
                _thermostatId = -1;
            }
        }

        public bool IsTransitionTypeNull
        {
            get { return _isTransitionTypeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isTransitionTypeNull = value;
                _transitionType = System.String.Empty;
            }
        }

        public bool IsTemperatureDeltaNull
        {
            get { return _isTemperatureDeltaNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isTemperatureDeltaNull = value;
                _temperatureDelta = -1;
            }
        }

        public bool IsMinutesPerDegreeNull
        {
            get { return _isMinutesPerDegreeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isMinutesPerDegreeNull = value;
                _minutesPerDegree = -1;
            }
        }

        public bool IsOccurancesNull
        {
            get { return _isOccurancesNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isOccurancesNull = value;
                _occurances = -1;
            }
        }


        #endregion

        #region Constructor
        public TransitionStat()
        {
        }
        #endregion

        #region Methods
        public static TransitionStat LoadTransitionStat(int transitionStatId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("LoadTransitionStat", ThermostatMonitorLib.Global.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", transitionStatId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetTransitionStat(row);
        }

        internal static TransitionStat GetTransitionStat(DataRow row)
        {
            TransitionStat result = new TransitionStat();
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

            if (row.Table.Columns.Contains("ThermostatId"))
            {
                if (Convert.IsDBNull(row["ThermostatId"]))
                {
                    result._isThermostatIdNull = true;
                }
                else
                {
                    result._thermostatId = Convert.ToInt32(row["ThermostatId"]);
                    result._isThermostatIdNull = false;
                }
            }

            if (row.Table.Columns.Contains("TransitionType"))
            {
                if (Convert.IsDBNull(row["TransitionType"]))
                {
                    result._isTransitionTypeNull = true;
                }
                else
                {
                    result._transitionType = Convert.ToString(row["TransitionType"]);
                    result._isTransitionTypeNull = false;
                }
            }

            if (row.Table.Columns.Contains("TemperatureDelta"))
            {
                if (Convert.IsDBNull(row["TemperatureDelta"]))
                {
                    result._isTemperatureDeltaNull = true;
                }
                else
                {
                    result._temperatureDelta = Convert.ToInt32(row["TemperatureDelta"]);
                    result._isTemperatureDeltaNull = false;
                }
            }

            if (row.Table.Columns.Contains("MinutesPerDegree"))
            {
                if (Convert.IsDBNull(row["MinutesPerDegree"]))
                {
                    result._isMinutesPerDegreeNull = true;
                }
                else
                {
                    result._minutesPerDegree = Convert.ToDouble(row["MinutesPerDegree"]);
                    result._isMinutesPerDegreeNull = false;
                }
            }

            if (row.Table.Columns.Contains("Occurances"))
            {
                if (Convert.IsDBNull(row["Occurances"]))
                {
                    result._isOccurancesNull = true;
                }
                else
                {
                    result._occurances = Convert.ToInt32(row["Occurances"]);
                    result._isOccurancesNull = false;
                }
            }

            return result;
        }

        public static int SaveTransitionStat(TransitionStat transitionStat)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveTransitionStat", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (transitionStat._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", transitionStat._id);
            }

            if (transitionStat._isThermostatIdNull)
            {
                cmd.Parameters.AddWithValue("@ThermostatId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ThermostatId", transitionStat._thermostatId);
            }

            if (transitionStat._isTransitionTypeNull)
            {
                cmd.Parameters.AddWithValue("@TransitionType", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TransitionType", transitionStat._transitionType);
            }

            if (transitionStat._isTemperatureDeltaNull)
            {
                cmd.Parameters.AddWithValue("@TemperatureDelta", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TemperatureDelta", transitionStat._temperatureDelta);
            }

            if (transitionStat._isMinutesPerDegreeNull)
            {
                cmd.Parameters.AddWithValue("@MinutesPerDegree", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MinutesPerDegree", transitionStat._minutesPerDegree);
            }

            if (transitionStat._isOccurancesNull)
            {
                cmd.Parameters.AddWithValue("@Occurances", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Occurances", transitionStat._occurances);
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
            transitionStat.Id = result;
            return result;
        }

        public static void DeleteTransitionStat(int transitionStatId)
        {
            SqlCommand cmd = new SqlCommand("DeleteTransitionStat", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", transitionStatId);
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
            return Utils.GetPropertyValue<TransitionStat>(this, propertyName);
        }

        #endregion

    }
}




