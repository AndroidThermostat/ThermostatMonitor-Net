using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ThermostatMonitorLib
{
    public partial class Location
    {
        public static Location LoadLocation(System.Guid apiKey)
        {
            Locations locations = Locations.LoadLocations("SELECT * FROM locations WHERE api_key=@ApiKey", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@ApiKey", apiKey) });
            if (locations.Count == 0) return null; else return locations[0];
        }
    }
}
