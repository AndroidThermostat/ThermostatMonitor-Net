using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Locations : System.Collections.Generic.List<Location>
    {

        #region Constructor
        public Locations()
        {
        }
        #endregion

        #region Methods
        public static Locations LoadLocations(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return Locations.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static Locations ConvertFromDT(DataTable dt)
        {
            Locations result = new Locations();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(Location.GetLocation(row));
            }
            return result;
        }

        public static Locations LoadAllLocations()
        {
            return Locations.LoadLocations("LoadLocationsAll", CommandType.StoredProcedure, null);
        }

        public Location GetLocationById(int locationId)
        {
            foreach (Location location in this)
            {
                if (location.Id == locationId) return location;
            }
            return null;
        }

        public static Locations LoadLocationsByUserId(System.Int32 userId)
        {
            return Locations.LoadLocations("LoadLocationsByUserId", CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@UserId", userId) });
        }


        public Locations Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            Locations result = new Locations();
            foreach (var i in sortedList) { result.Add((Location)i); }
            return result;
        }

        #endregion


    }
}

