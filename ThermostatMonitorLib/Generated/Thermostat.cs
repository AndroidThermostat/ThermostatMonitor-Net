using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Thermostat
    {
        #region Declarations
        System.Int32 _id;
        System.String _ipAddress;
        System.String _displayName;
        System.Double _aCTons;
        System.Int32 _aCSeer;
        System.Double _aCKilowatts;
        System.Double _fanKilowatts;
        System.String _brand;
        System.Int32 _locationId;
        System.Double _heatBtuPerHour;
        System.String _keyName;

        bool _isIdNull = true;
        bool _isIpAddressNull = true;
        bool _isDisplayNameNull = true;
        bool _isACTonsNull = true;
        bool _isACSeerNull = true;
        bool _isACKilowattsNull = true;
        bool _isFanKilowattsNull = true;
        bool _isBrandNull = true;
        bool _isLocationIdNull = true;
        bool _isHeatBtuPerHourNull = true;
        bool _isKeyNameNull = true;

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

        public System.String IpAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                _isIpAddressNull = false;
            }
        }

        public System.String DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                _isDisplayNameNull = false;
            }
        }

        public System.Double ACTons
        {
            get { return _aCTons; }
            set
            {
                _aCTons = value;
                _isACTonsNull = false;
            }
        }

        public System.Int32 ACSeer
        {
            get { return _aCSeer; }
            set
            {
                _aCSeer = value;
                _isACSeerNull = false;
            }
        }

        public System.Double ACKilowatts
        {
            get { return _aCKilowatts; }
            set
            {
                _aCKilowatts = value;
                _isACKilowattsNull = false;
            }
        }

        public System.Double FanKilowatts
        {
            get { return _fanKilowatts; }
            set
            {
                _fanKilowatts = value;
                _isFanKilowattsNull = false;
            }
        }

        public System.String Brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                _isBrandNull = false;
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

        public System.Double HeatBtuPerHour
        {
            get { return _heatBtuPerHour; }
            set
            {
                _heatBtuPerHour = value;
                _isHeatBtuPerHourNull = false;
            }
        }

        public System.String KeyName
        {
            get { return _keyName; }
            set
            {
                _keyName = value;
                _isKeyNameNull = false;
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

        public bool IsIpAddressNull
        {
            get { return _isIpAddressNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isIpAddressNull = value;
                _ipAddress = System.String.Empty;
            }
        }

        public bool IsDisplayNameNull
        {
            get { return _isDisplayNameNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isDisplayNameNull = value;
                _displayName = System.String.Empty;
            }
        }

        public bool IsACTonsNull
        {
            get { return _isACTonsNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isACTonsNull = value;
                _aCTons = -1;
            }
        }

        public bool IsACSeerNull
        {
            get { return _isACSeerNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isACSeerNull = value;
                _aCSeer = -1;
            }
        }

        public bool IsACKilowattsNull
        {
            get { return _isACKilowattsNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isACKilowattsNull = value;
                _aCKilowatts = -1;
            }
        }

        public bool IsFanKilowattsNull
        {
            get { return _isFanKilowattsNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isFanKilowattsNull = value;
                _fanKilowatts = -1;
            }
        }

        public bool IsBrandNull
        {
            get { return _isBrandNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isBrandNull = value;
                _brand = System.String.Empty;
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

        public bool IsHeatBtuPerHourNull
        {
            get { return _isHeatBtuPerHourNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isHeatBtuPerHourNull = value;
                _heatBtuPerHour = -1;
            }
        }

        public bool IsKeyNameNull
        {
            get { return _isKeyNameNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isKeyNameNull = value;
                _keyName = System.String.Empty;
            }
        }


        #endregion

        #region Constructor
        public Thermostat()
        {
        }
        #endregion

        #region Methods
        public static Thermostat LoadThermostat(int thermostatId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("LoadThermostat", ThermostatMonitorLib.Global.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", thermostatId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetThermostat(row);
        }

        internal static Thermostat GetThermostat(DataRow row)
        {
            Thermostat result = new Thermostat();
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

            if (row.Table.Columns.Contains("IpAddress"))
            {
                if (Convert.IsDBNull(row["IpAddress"]))
                {
                    result._isIpAddressNull = true;
                }
                else
                {
                    result._ipAddress = Convert.ToString(row["IpAddress"]);
                    result._isIpAddressNull = false;
                }
            }

            if (row.Table.Columns.Contains("DisplayName"))
            {
                if (Convert.IsDBNull(row["DisplayName"]))
                {
                    result._isDisplayNameNull = true;
                }
                else
                {
                    result._displayName = Convert.ToString(row["DisplayName"]);
                    result._isDisplayNameNull = false;
                }
            }

            if (row.Table.Columns.Contains("ACTons"))
            {
                if (Convert.IsDBNull(row["ACTons"]))
                {
                    result._isACTonsNull = true;
                }
                else
                {
                    result._aCTons = Convert.ToDouble(row["ACTons"]);
                    result._isACTonsNull = false;
                }
            }

            if (row.Table.Columns.Contains("ACSeer"))
            {
                if (Convert.IsDBNull(row["ACSeer"]))
                {
                    result._isACSeerNull = true;
                }
                else
                {
                    result._aCSeer = Convert.ToInt32(row["ACSeer"]);
                    result._isACSeerNull = false;
                }
            }

            if (row.Table.Columns.Contains("ACKilowatts"))
            {
                if (Convert.IsDBNull(row["ACKilowatts"]))
                {
                    result._isACKilowattsNull = true;
                }
                else
                {
                    result._aCKilowatts = Convert.ToDouble(row["ACKilowatts"]);
                    result._isACKilowattsNull = false;
                }
            }

            if (row.Table.Columns.Contains("FanKilowatts"))
            {
                if (Convert.IsDBNull(row["FanKilowatts"]))
                {
                    result._isFanKilowattsNull = true;
                }
                else
                {
                    result._fanKilowatts = Convert.ToDouble(row["FanKilowatts"]);
                    result._isFanKilowattsNull = false;
                }
            }

            if (row.Table.Columns.Contains("Brand"))
            {
                if (Convert.IsDBNull(row["Brand"]))
                {
                    result._isBrandNull = true;
                }
                else
                {
                    result._brand = Convert.ToString(row["Brand"]);
                    result._isBrandNull = false;
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

            if (row.Table.Columns.Contains("HeatBtuPerHour"))
            {
                if (Convert.IsDBNull(row["HeatBtuPerHour"]))
                {
                    result._isHeatBtuPerHourNull = true;
                }
                else
                {
                    result._heatBtuPerHour = Convert.ToDouble(row["HeatBtuPerHour"]);
                    result._isHeatBtuPerHourNull = false;
                }
            }

            if (row.Table.Columns.Contains("KeyName"))
            {
                if (Convert.IsDBNull(row["KeyName"]))
                {
                    result._isKeyNameNull = true;
                }
                else
                {
                    result._keyName = Convert.ToString(row["KeyName"]);
                    result._isKeyNameNull = false;
                }
            }

            return result;
        }

        public static int SaveThermostat(Thermostat thermostat)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveThermostat", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (thermostat._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", thermostat._id);
            }

            if (thermostat._isIpAddressNull)
            {
                cmd.Parameters.AddWithValue("@IpAddress", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IpAddress", thermostat._ipAddress);
            }

            if (thermostat._isDisplayNameNull)
            {
                cmd.Parameters.AddWithValue("@DisplayName", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DisplayName", thermostat._displayName);
            }

            if (thermostat._isACTonsNull)
            {
                cmd.Parameters.AddWithValue("@ACTons", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ACTons", thermostat._aCTons);
            }

            if (thermostat._isACSeerNull)
            {
                cmd.Parameters.AddWithValue("@ACSeer", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ACSeer", thermostat._aCSeer);
            }

            if (thermostat._isACKilowattsNull)
            {
                cmd.Parameters.AddWithValue("@ACKilowatts", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ACKilowatts", thermostat._aCKilowatts);
            }

            if (thermostat._isFanKilowattsNull)
            {
                cmd.Parameters.AddWithValue("@FanKilowatts", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FanKilowatts", thermostat._fanKilowatts);
            }

            if (thermostat._isBrandNull)
            {
                cmd.Parameters.AddWithValue("@Brand", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Brand", thermostat._brand);
            }

            if (thermostat._isLocationIdNull)
            {
                cmd.Parameters.AddWithValue("@LocationId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationId", thermostat._locationId);
            }

            if (thermostat._isHeatBtuPerHourNull)
            {
                cmd.Parameters.AddWithValue("@HeatBtuPerHour", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@HeatBtuPerHour", thermostat._heatBtuPerHour);
            }

            if (thermostat._isKeyNameNull)
            {
                cmd.Parameters.AddWithValue("@KeyName", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@KeyName", thermostat._keyName);
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
            thermostat.Id = result;
            return result;
        }

        public static void DeleteThermostat(int thermostatId)
        {
            SqlCommand cmd = new SqlCommand("DeleteThermostat", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", thermostatId);
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
            return Utils.GetPropertyValue<Thermostat>(this, propertyName);
        }

        #endregion

    }
}




