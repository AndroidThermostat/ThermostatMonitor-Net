


using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Location
    {
        #region Declarations
        int _id;
        int _userId;
        string _name;
        string _apiKey;
        string _zipCode;
        double _electricityPrice;
        bool _shareData;
        int _timezone;
        bool _daylightSavings;
        double _heatFuelPrice;
        int _openWeatherCityId;

        bool _isIdNull = true;
        bool _isUserIdNull = true;
        bool _isNameNull = true;
        bool _isApiKeyNull = true;
        bool _isZipCodeNull = true;
        bool _isElectricityPriceNull = true;
        bool _isShareDataNull = true;
        bool _isTimezoneNull = true;
        bool _isDaylightSavingsNull = true;
        bool _isHeatFuelPriceNull = true;
        bool _isOpenWeatherCityIdNull = true;

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

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                _isUserIdNull = false;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _isNameNull = false;
            }
        }

        public string ApiKey
        {
            get { return _apiKey; }
            set
            {
                _apiKey = value;
                _isApiKeyNull = false;
            }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
                _isZipCodeNull = false;
            }
        }

        public double ElectricityPrice
        {
            get { return _electricityPrice; }
            set
            {
                _electricityPrice = value;
                _isElectricityPriceNull = false;
            }
        }

        public bool ShareData
        {
            get { return _shareData; }
            set
            {
                _shareData = value;
                _isShareDataNull = false;
            }
        }

        public int Timezone
        {
            get { return _timezone; }
            set
            {
                _timezone = value;
                _isTimezoneNull = false;
            }
        }

        public bool DaylightSavings
        {
            get { return _daylightSavings; }
            set
            {
                _daylightSavings = value;
                _isDaylightSavingsNull = false;
            }
        }

        public double HeatFuelPrice
        {
            get { return _heatFuelPrice; }
            set
            {
                _heatFuelPrice = value;
                _isHeatFuelPriceNull = false;
            }
        }

        public int OpenWeatherCityId
        {
            get { return _openWeatherCityId; }
            set
            {
                _openWeatherCityId = value;
                _isOpenWeatherCityIdNull = false;
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

        public bool IsUserIdNull
        {
            get { return _isUserIdNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isUserIdNull = value;
                _userId = -1;
            }
        }

        public bool IsNameNull
        {
            get { return _isNameNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isNameNull = value;
                _name = System.String.Empty;
            }
        }

        public bool IsApiKeyNull
        {
            get { return _isApiKeyNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isApiKeyNull = value;
                _apiKey = System.String.Empty;
            }
        }

        public bool IsZipCodeNull
        {
            get { return _isZipCodeNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isZipCodeNull = value;
                _zipCode = System.String.Empty;
            }
        }

        public bool IsElectricityPriceNull
        {
            get { return _isElectricityPriceNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isElectricityPriceNull = value;
                _electricityPrice = -1;
            }
        }

        public bool IsShareDataNull
        {
            get { return _isShareDataNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isShareDataNull = value;
                _shareData = false;
            }
        }

        public bool IsTimezoneNull
        {
            get { return _isTimezoneNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isTimezoneNull = value;
                _timezone = -1;
            }
        }

        public bool IsDaylightSavingsNull
        {
            get { return _isDaylightSavingsNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isDaylightSavingsNull = value;
                _daylightSavings = false;
            }
        }

        public bool IsHeatFuelPriceNull
        {
            get { return _isHeatFuelPriceNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isHeatFuelPriceNull = value;
                _heatFuelPrice = -1;
            }
        }

        public bool IsOpenWeatherCityIdNull
        {
            get { return _isOpenWeatherCityIdNull; }
            set
            {
                if (!value) throw new Exception("Can not set this property to false");
                _isOpenWeatherCityIdNull = value;
                _openWeatherCityId = -1;
            }
        }


        #endregion


        #region Constructor
        public Location()
        {
        }
        #endregion

        #region Methods
        public static Location LoadLocation(int locationId)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("locations_load", ThermostatMonitorLib.Global.MySqlConnection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Id", locationId);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return GetLocation(row);
        }

        internal static Location GetLocation(DataRow row)
        {
            Location result = new Location();
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

            if (row.Table.Columns.Contains("user_id"))
            {
                if (Convert.IsDBNull(row["user_id"]))
                {
                    result._isUserIdNull = true;
                }
                else
                {
                    result._userId = Convert.ToInt32(row["user_id"]);
                    result._isUserIdNull = false;
                }
            }

            if (row.Table.Columns.Contains("name"))
            {
                if (Convert.IsDBNull(row["name"]))
                {
                    result._isNameNull = true;
                }
                else
                {
                    result._name = Convert.ToString(row["name"]);
                    result._isNameNull = false;
                }
            }

            if (row.Table.Columns.Contains("api_key"))
            {
                if (Convert.IsDBNull(row["api_key"]))
                {
                    result._isApiKeyNull = true;
                }
                else
                {
                    result._apiKey = Convert.ToString(row["api_key"]);
                    result._isApiKeyNull = false;
                }
            }

            if (row.Table.Columns.Contains("zip_code"))
            {
                if (Convert.IsDBNull(row["zip_code"]))
                {
                    result._isZipCodeNull = true;
                }
                else
                {
                    result._zipCode = Convert.ToString(row["zip_code"]);
                    result._isZipCodeNull = false;
                }
            }

            if (row.Table.Columns.Contains("electricity_price"))
            {
                if (Convert.IsDBNull(row["electricity_price"]))
                {
                    result._isElectricityPriceNull = true;
                }
                else
                {
                    result._electricityPrice = Convert.ToDouble(row["electricity_price"]);
                    result._isElectricityPriceNull = false;
                }
            }

            if (row.Table.Columns.Contains("share_data"))
            {
                if (Convert.IsDBNull(row["share_data"]))
                {
                    result._isShareDataNull = true;
                }
                else
                {
                    result._shareData = Convert.ToBoolean(row["share_data"]);
                    result._isShareDataNull = false;
                }
            }

            if (row.Table.Columns.Contains("timezone"))
            {
                if (Convert.IsDBNull(row["timezone"]))
                {
                    result._isTimezoneNull = true;
                }
                else
                {
                    result._timezone = Convert.ToInt32(row["timezone"]);
                    result._isTimezoneNull = false;
                }
            }

            if (row.Table.Columns.Contains("daylight_savings"))
            {
                if (Convert.IsDBNull(row["daylight_savings"]))
                {
                    result._isDaylightSavingsNull = true;
                }
                else
                {
                    result._daylightSavings = Convert.ToBoolean(row["daylight_savings"]);
                    result._isDaylightSavingsNull = false;
                }
            }

            if (row.Table.Columns.Contains("heat_fuel_price"))
            {
                if (Convert.IsDBNull(row["heat_fuel_price"]))
                {
                    result._isHeatFuelPriceNull = true;
                }
                else
                {
                    result._heatFuelPrice = Convert.ToDouble(row["heat_fuel_price"]);
                    result._isHeatFuelPriceNull = false;
                }
            }

            if (row.Table.Columns.Contains("open_weather_city_id"))
            {
                if (Convert.IsDBNull(row["open_weather_city_id"]))
                {
                    result._isOpenWeatherCityIdNull = true;
                }
                else
                {
                    result._openWeatherCityId = Convert.ToInt32(row["open_weather_city_id"]);
                    result._isOpenWeatherCityIdNull = false;
                }
            }

            return result;
        }

        public static int SaveLocation(Location location)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand("locations_save", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (location._isIdNull)
            {
                cmd.Parameters.AddWithValue("@id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", location._id);
            }

            if (location._isUserIdNull)
            {
                cmd.Parameters.AddWithValue("@user_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@user_id", location._userId);
            }

            if (location._isNameNull)
            {
                cmd.Parameters.AddWithValue("@name", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@name", location._name);
            }

            if (location._isApiKeyNull)
            {
                cmd.Parameters.AddWithValue("@api_key", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@api_key", location._apiKey);
            }

            if (location._isZipCodeNull)
            {
                cmd.Parameters.AddWithValue("@zip_code", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@zip_code", location._zipCode);
            }

            if (location._isElectricityPriceNull)
            {
                cmd.Parameters.AddWithValue("@electricity_price", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@electricity_price", location._electricityPrice);
            }

            if (location._isShareDataNull)
            {
                cmd.Parameters.AddWithValue("@share_data", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@share_data", location._shareData);
            }

            if (location._isTimezoneNull)
            {
                cmd.Parameters.AddWithValue("@timezone", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@timezone", location._timezone);
            }

            if (location._isDaylightSavingsNull)
            {
                cmd.Parameters.AddWithValue("@daylight_savings", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@daylight_savings", location._daylightSavings);
            }

            if (location._isHeatFuelPriceNull)
            {
                cmd.Parameters.AddWithValue("@heat_fuel_price", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@heat_fuel_price", location._heatFuelPrice);
            }

            if (location._isOpenWeatherCityIdNull)
            {
                cmd.Parameters.AddWithValue("@open_weather_city_id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@open_weather_city_id", location._openWeatherCityId);
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
            location.Id = result;
            return result;
        }

        public static void DeleteLocation(int locationId)
        {
            MySqlCommand cmd = new MySqlCommand("locations_delete", ThermostatMonitorLib.Global.MySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", locationId);
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
            return Utils.GetPropertyValue<Location>(this, propertyName);
        }

        #endregion

    }
}


