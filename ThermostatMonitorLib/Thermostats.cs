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
            string sql = "select t.Id,t.LocationId,t.AcTons,t.AcSeer,t.ACKilowatts,t.FanKilowatts,t.HeatBtuPerHour,l.ZipCode,l.ElectricityPrice,l.Timezone from thermostats t inner join locations l on l.id=t.locationid WHERE l.ShareData=1 and (SELECT COUNT(*) FROM Temperatures WHERE ThermostatId=t.Id)>0";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Global.Connection);
            DataTable result = new DataTable();
            adapter.Fill(result);
            return result;
        }

    }
}
