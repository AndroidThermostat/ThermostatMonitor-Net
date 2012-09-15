using System;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Location
    {
        #region Declarations
        System.Int32 _id;
        System.Int32 _userId;
        System.String _name;
        System.Guid _apiKey;
        System.String _zipCode;
        System.Double _electricityPrice;
        System.Boolean _shareData;
        System.Int32 _timezone;
        System.Boolean _daylightSavings;
        System.Double _heatFuelPrice;
        System.Int32 _openWeatherCityId;

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
        public System.Int32 Id
        {
            get { return _id; }
            set
            {
                _id = value;
                _isIdNull = false;
            }
        }

        public System.Int32 UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                _isUserIdNull = false;
            }
        }

        public System.String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _isNameNull = false;
            }
        }

        public System.Guid ApiKey
        {
            get { return _apiKey; }
            set
            {
                _apiKey = value;
                _isApiKeyNull = false;
            }
        }

        public System.String ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
                _isZipCodeNull = false;
            }
        }

        public System.Double ElectricityPrice
        {
            get { return _electricityPrice; }
            set
            {
                _electricityPrice = value;
                _isElectricityPriceNull = false;
            }
        }

        public System.Boolean ShareData
        {
            get { return _shareData; }
            set
            {
                _shareData = value;
                _isShareDataNull = false;
            }
        }

        public System.Int32 Timezone
        {
            get { return _timezone; }
            set
            {
                _timezone = value;
                _isTimezoneNull = false;
            }
        }

        public System.Boolean DaylightSavings
        {
            get { return _daylightSavings; }
            set
            {
                _daylightSavings = value;
                _isDaylightSavingsNull = false;
            }
        }

        public System.Double HeatFuelPrice
        {
            get { return _heatFuelPrice; }
            set
            {
                _heatFuelPrice = value;
                _isHeatFuelPriceNull = false;
            }
        }

        public System.Int32 OpenWeatherCityId
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
                _apiKey = System.Guid.Empty;
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
            SqlDataAdapter adapter = new SqlDataAdapter("LoadLocation", ThermostatMonitorLib.Global.Connection);
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

            if (row.Table.Columns.Contains("UserId"))
            {
                if (Convert.IsDBNull(row["UserId"]))
                {
                    result._isUserIdNull = true;
                }
                else
                {
                    result._userId = Convert.ToInt32(row["UserId"]);
                    result._isUserIdNull = false;
                }
            }

            if (row.Table.Columns.Contains("Name"))
            {
                if (Convert.IsDBNull(row["Name"]))
                {
                    result._isNameNull = true;
                }
                else
                {
                    result._name = Convert.ToString(row["Name"]);
                    result._isNameNull = false;
                }
            }

            if (row.Table.Columns.Contains("ApiKey"))
            {
                if (Convert.IsDBNull(row["ApiKey"]))
                {
                    result._isApiKeyNull = true;
                }
                else
                {
                    result._apiKey = (System.Guid)(row["ApiKey"]);
                    result._isApiKeyNull = false;
                }
            }

            if (row.Table.Columns.Contains("ZipCode"))
            {
                if (Convert.IsDBNull(row["ZipCode"]))
                {
                    result._isZipCodeNull = true;
                }
                else
                {
                    result._zipCode = Convert.ToString(row["ZipCode"]);
                    result._isZipCodeNull = false;
                }
            }

            if (row.Table.Columns.Contains("ElectricityPrice"))
            {
                if (Convert.IsDBNull(row["ElectricityPrice"]))
                {
                    result._isElectricityPriceNull = true;
                }
                else
                {
                    result._electricityPrice = Convert.ToDouble(row["ElectricityPrice"]);
                    result._isElectricityPriceNull = false;
                }
            }

            if (row.Table.Columns.Contains("ShareData"))
            {
                if (Convert.IsDBNull(row["ShareData"]))
                {
                    result._isShareDataNull = true;
                }
                else
                {
                    result._shareData = Convert.ToBoolean(row["ShareData"]);
                    result._isShareDataNull = false;
                }
            }

            if (row.Table.Columns.Contains("Timezone"))
            {
                if (Convert.IsDBNull(row["Timezone"]))
                {
                    result._isTimezoneNull = true;
                }
                else
                {
                    result._timezone = Convert.ToInt32(row["Timezone"]);
                    result._isTimezoneNull = false;
                }
            }

            if (row.Table.Columns.Contains("DaylightSavings"))
            {
                if (Convert.IsDBNull(row["DaylightSavings"]))
                {
                    result._isDaylightSavingsNull = true;
                }
                else
                {
                    result._daylightSavings = Convert.ToBoolean(row["DaylightSavings"]);
                    result._isDaylightSavingsNull = false;
                }
            }

            if (row.Table.Columns.Contains("HeatFuelPrice"))
            {
                if (Convert.IsDBNull(row["HeatFuelPrice"]))
                {
                    result._isHeatFuelPriceNull = true;
                }
                else
                {
                    result._heatFuelPrice = Convert.ToDouble(row["HeatFuelPrice"]);
                    result._isHeatFuelPriceNull = false;
                }
            }

            if (row.Table.Columns.Contains("OpenWeatherCityId"))
            {
                if (Convert.IsDBNull(row["OpenWeatherCityId"]))
                {
                    result._isOpenWeatherCityIdNull = true;
                }
                else
                {
                    result._openWeatherCityId = Convert.ToInt32(row["OpenWeatherCityId"]);
                    result._isOpenWeatherCityIdNull = false;
                }
            }

            return result;
        }

        public static int SaveLocation(Location location)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SaveLocation", ThermostatMonitorLib.Global.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if (location._isIdNull)
            {
                cmd.Parameters.AddWithValue("@Id", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Id", location._id);
            }

            if (location._isUserIdNull)
            {
                cmd.Parameters.AddWithValue("@UserId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@UserId", location._userId);
            }

            if (location._isNameNull)
            {
                cmd.Parameters.AddWithValue("@Name", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Name", location._name);
            }

            if (location._isApiKeyNull)
            {
                cmd.Parameters.AddWithValue("@ApiKey", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ApiKey", location._apiKey);
            }

            if (location._isZipCodeNull)
            {
                cmd.Parameters.AddWithValue("@ZipCode", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ZipCode", location._zipCode);
            }

            if (location._isElectricityPriceNull)
            {
                cmd.Parameters.AddWithValue("@ElectricityPrice", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ElectricityPrice", location._electricityPrice);
            }

            if (location._isShareDataNull)
            {
                cmd.Parameters.AddWithValue("@ShareData", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ShareData", location._shareData);
            }

            if (location._isTimezoneNull)
            {
                cmd.Parameters.AddWithValue("@Timezone", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Timezone", location._timezone);
            }

            if (location._isDaylightSavingsNull)
            {
                cmd.Parameters.AddWithValue("@DaylightSavings", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DaylightSavings", location._daylightSavings);
            }

            if (location._isHeatFuelPriceNull)
            {
                cmd.Parameters.AddWithValue("@HeatFuelPrice", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@HeatFuelPrice", location._heatFuelPrice);
            }

            if (location._isOpenWeatherCityIdNull)
            {
                cmd.Parameters.AddWithValue("@OpenWeatherCityId", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OpenWeatherCityId", location._openWeatherCityId);
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
            SqlCommand cmd = new SqlCommand("DeleteLocation", ThermostatMonitorLib.Global.Connection);
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




