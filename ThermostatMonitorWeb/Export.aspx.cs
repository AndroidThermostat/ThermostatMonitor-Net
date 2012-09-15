using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Export : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DeleteOldData();

        DataTable thermostats=ThermostatMonitorLib.Thermostats.LoadPublicThermostats();
        OutputCSV(ThermostatMonitorLib.Utils.DataTableToCSV(thermostats), "/dump/thermostats.csv");

        foreach (DataRow row in thermostats.Rows)
        {
            int id = Convert.ToInt32(row["Id"]);
            int locationId = Convert.ToInt32(row["LocationId"]);
            double acKilowatts = Convert.ToDouble(row["ACKilowatts"]);
            double fanKilowatts = Convert.ToDouble(row["FanKilowatts"]);
            double heatBTU = 0;
            try
            {
                heatBTU = Convert.ToDouble(row["HeatBtuPerHour"]);
            }
            catch { }

            ThermostatMonitorLib.Cycles cycles = ThermostatMonitorLib.Cycles.LoadRange(id, new DateTime(2000, 1, 1), DateTime.Now);
            OutputCSV(cycles.GetCSV(acKilowatts, fanKilowatts, heatBTU, 0), "/dump/t" + id.ToString() + "_cycles.csv");
            OutputCSV(ThermostatMonitorLib.Temperatures.LoadTemperaturesByThermostatId(id).GetCSV(), "/dump/t" + id.ToString() + "_inside.csv");

            OutputCSV(ThermostatMonitorLib.OutsideConditions.LoadOutsideConditionsByLocationId(locationId).GetCSV(), "/dump/l" + locationId.ToString() + "_outside.csv");
        }

        ZipFiles();
        Response.Redirect("/dump/export.zip");

    }

    private void ZipFiles()
    {
        string path=Server.MapPath("/dump/");
        
        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
        {
            foreach (string file in System.IO.Directory.GetFiles(path,"*.csv"))
            {
                zip.AddFile(file,"/");
            }
            zip.Save(path + "export.zip");
        }
    }

    private void DeleteOldData()
    {
        string path=Server.MapPath("/dump/");
        foreach (string file in System.IO.Directory.GetFiles(path)) { System.IO.File.Delete(file); }
    }

    private void OutputCSV(string csv, string fileName)
    {
        System.IO.File.WriteAllText(Server.MapPath(fileName), csv);
    }

}
