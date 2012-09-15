using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    public partial class Location
    {
        public static Location LoadLocation(System.Guid apiKey)
        {
            Locations locations = Locations.LoadLocations("SELECT * FROM Locations WHERE ApiKey=@ApiKey", CommandType.Text, new SqlParameter[] { new SqlParameter("@ApiKey", apiKey) });
            if (locations.Count == 0) return null; else return locations[0];
        }
    }
}
