using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ThermostatMonitorLib
{
    public partial class Thermostats
    {

        public static DataTable LoadPublicThermostats()
        {
            string sql = "select t.Id,t.location_id,t.ac_tons,t.ac_seer,t.ac_kilowatts,t.fan_kilowatts,t.heat_btu_per_hour,l.zip_code,l.electricity_price,l.timezone from thermostats t inner join locations l on l.id=t.location_id WHERE l.share_data=1 and (SELECT COUNT(*) FROM temperatures WHERE thermostat_id=t.Id)>0";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Global.Connection);
            DataTable result = new DataTable();
            adapter.Fill(result);
            return result;
        }

    }
}
