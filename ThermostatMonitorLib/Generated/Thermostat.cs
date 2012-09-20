


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Thermostat
    {
        #region Declarations
        int _id;
        string _ipAddress;
        string _displayName;
        double _acTons;
        int _acSeer;
        double _acKilowatts;
        double _fanKilowatts;
        string _brand;
        int _locationId;
        double _heatBtuPerHour;
        string _keyName;

        bool _isIdNull = true;
        bool _isIpAddressNull = true;
        bool _isDisplayNameNull = true;
        bool _isAcTonsNull = true;
        bool _isAcSeerNull = true;
        bool _isAcKilowattsNull = true;
        bool _isFanKilowattsNull = true;
        bool _isBrandNull = true;
        bool _isLocationIdNull = true;
        bool _isHeatBtuPerHourNull = true;
        bool _isKeyNameNull = true;

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

        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                _isIpAddressNull = false;
            }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                _isDisplayNameNull = false;
            }
        }

        public double AcTons
        {
            get { return _acTons; }
            set
            {
                _acTons = value;
                _isAcTonsNull = false;
            }
        }

        public int AcSeer
        {
            get { return _acSeer; }
            set
            {
                _acSeer = value;
                _isAcSeerNull = false;
            }
        }

        public double AcKilowatts
        {
            get { return _acKilowatts; }
            set
            {
                _acKilowatts = value;
                _isAcKilowattsNull = false;
            }
        }

        public double FanKilowatts
        {
            get { return _fanKilowatts; }
            set
            {
                _fanKilowatts = value;
                _isFanKilowattsNull = false;
            }
        }

        public string Brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                _isBrandNull = false;
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

        public double HeatBtuPerHour
        {
            get { return _heatBtuPerHour; }
            set
            {
                _heatBtuPerHour = value;
                _isHeatBtuPerHourNull = false;
            }
        }

        public string KeyName
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

        public bool IsAcTonsNull
        {
            get { return _isAcTonsNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isAcTonsNull = value;
                _acTons = -1;
            }
        }

        public bool IsAcSeerNull
        {
            get { return _isAcSeerNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isAcSeerNull = value;
                _acSeer = -1;
            }
        }

        public bool IsAcKilowattsNull
        {
            get { return _isAcKilowattsNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isAcKilowattsNull = value;
                _acKilowatts = -1;
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
            MySqlDataAdapter adapter = new MySqlDataAdapter("thermostats_load", ThermostatMonitorLib.Global.MySqlConnection);
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

            if (row.Table.Columns.Contains("ip_address"))
            {
                if (Convert.IsDBNull(row["ip_address"]))
                {
                    result._isIpAddressNull = true;
                }
                else
                {
                    result._ipAddress = Convert.ToString(row["ip_address"]);
                    result._isIpAddressNull = false;
                }
            }

            if (row.Table.Columns.Contains("display_name"))
            {
                if (Convert.IsDBNull(row["display_name"]))
                {
                    result._isDisplayNameNull = true;
                }
                else
                {
                    result._displayName = Convert.ToString(row["display_name"]);
                    result._isDisplayNameNull = false;
                }
            }

            if (row.Table.Columns.Contains("ac_tons"))
            {
                if (Convert.IsDBNull(row["ac_tons"]))
                {
                    result._isAcTonsNull = true;
                }
                else
                {
                    result._acTons = Convert.ToDouble(row["ac_tons"]);
                    result._isAcTonsNull = false;
                }
            }

            if (row.Table.Columns.Contains("ac_seer"))
            {
                if (Convert.IsDBNull(row["ac_seer"]))
                {
                    result._isAcSeerNull = true;
                }
                else
                {
                    result._acSeer = Convert.ToInt32(row["ac_seer"]);
                    result._isAcSeerNull = false;
                }
            }

            if (row.Table.Columns.Contains("ac_kilowatts"))
            {
                if (Convert.IsDBNull(row["ac_kilowatts"]))
                {
                    result._isAcKilowattsNull = true;
                }
                else
                {
                    result._acKilowatts = Convert.ToDouble(row["ac_kilowatts"]);
                    result._isAcKilowattsNull = false;
                }
            }

            if (row.Table.Columns.Contains("fan_kilowatts"))
            {
                if (Convert.IsDBNull(row["fan_kilowatts"]))
                {
                    result._isFanKilowattsNull = true;
                }
                else
                {
                    result._fanKilowatts = Convert.ToDouble(row["fan_kilowatts"]);
                    result._isFanKilowattsNull = false;
                }
            }

            if (row.Table.Columns.Contains("brand"))
            {
                if (Convert.IsDBNull(row["brand"]))
                {
                    result._isBrandNull = true;
                }
                else
                {
                    result._brand = Convert.ToString(row["brand"]);
                    result._isBrandNull = false;
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

            if (row.Table.Columns.Contains("heat_btu_per_hour"))
            {
                if (Convert.IsDBNull(row["heat_btu_per_hour"]))
                {
                    result._isHeatBtuPerHourNull = true;
                }
                else
                {
                    result._heatBtuPerHour = Convert.ToDouble(row["heat_btu_per_hour"]);
                    result._isHeatBtuPerHourNull = false;
                }
            }

            if (row.Table.Columns.Contains("key_name"))
            {
                if (Convert.IsDBNull(row["key_name"]))
                {
                    result._isKeyNameNull = true;
                }
                else
                {
                    result._keyName = Convert.ToString(row["key_name"]);
                    result._isKeyNameNull = false;
                }
            }

            return result;
        }

        public static int SaveThermostat(Thermostat thermostat)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("thermostats_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (thermostat._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", thermostat._id);
            }

            if (thermostat._isIpAddressNull)
            {
                cmd.Parameters.AddWithValue("@ip_address", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ip_address", thermostat._ipAddress);
            }

            if (thermostat._isDisplayNameNull)
            {
                cmd.Parameters.AddWithValue("@display_name", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@display_name", thermostat._displayName);
            }

            if (thermostat._isAcTonsNull)
            {
                cmd.Parameters.AddWithValue("@ac_tons", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ac_tons", thermostat._acTons);
            }

            if (thermostat._isAcSeerNull)
            {
                cmd.Parameters.AddWithValue("@ac_seer", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ac_seer", thermostat._acSeer);
            }

            if (thermostat._isAcKilowattsNull)
            {
                cmd.Parameters.AddWithValue("@ac_kilowatts", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ac_kilowatts", thermostat._acKilowatts);
            }

            if (thermostat._isFanKilowattsNull)
            {
                cmd.Parameters.AddWithValue("@fan_kilowatts", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fan_kilowatts", thermostat._fanKilowatts);
            }

            if (thermostat._isBrandNull)
            {
                cmd.Parameters.AddWithValue("@brand", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@brand", thermostat._brand);
            }

            if (thermostat._isLocationIdNull)
            {
                cmd.Parameters.AddWithValue("@location_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@location_id", thermostat._locationId);
            }

            if (thermostat._isHeatBtuPerHourNull)
            {
                cmd.Parameters.AddWithValue("@heat_btu_per_hour", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@heat_btu_per_hour", thermostat._heatBtuPerHour);
            }

            if (thermostat._isKeyNameNull)
            {
                cmd.Parameters.AddWithValue("@key_name", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@key_name", thermostat._keyName);
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
            MySqlCommand cmd = new MySqlCommand("thermostats_delete", ThermostatMonitorLib.Global.MySqlConnection);
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


