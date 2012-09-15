using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Api_v1_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ThermostatMonitorLib.Location location=null;
        try
        {
            string apiKey = Request["apikey"];
            System.Guid apiKeyGuid = new Guid(apiKey);
            location = ThermostatMonitorLib.Location.LoadLocation(apiKeyGuid);
        }
        catch { }
        if (location == null) return;

        string action = Request["action"];
        switch (action.ToLower())
        {
            case "cyclestart":
                LogCycle(true, location.Id);
                break;
            case "cycleend":
                LogCycle(false, location.Id);
                break;
            case "loadsettings":
                LoadSettings(location);
                break;
            case "temperaturechanged":
                LogTemperature(location.Id);
                break;
            case "weatherchanged":
                LogWeather(location.Id);
                break;
            case "commands":
                LoadCommands(location.Id);
                break;
            case "settemperature":
                SetTemperature(location.Id);
                break;
        }
    }

    private void LoadCommands(int locationId)
    {
        Commands commands = Commands.Get(locationId);
        if (commands != null)
        {
            System.Collections.ArrayList al = new ArrayList();
            foreach (Command command in commands)
            {
                Hashtable c = new Hashtable();
                c["commandName"] = command.CommandName;
                c["thermostatIP"] = command.ThermosatIP;
                c["commandData"] = command.CommandData;
                al.Add(c);
            }
            Commands.Remove(locationId);
            Response.Write(ThermostatMonitorLib.JSON.JsonEncode(al));
        }
        Response.End();
    }
    
    private void LoadSettings(ThermostatMonitorLib.Location location)
    {
        //Build array list of thermostats
        ThermostatMonitorLib.Thermostats thermostats = ThermostatMonitorLib.Thermostats.LoadThermostatsByLocationId(location.Id);
        ArrayList al = new ArrayList();
        foreach (ThermostatMonitorLib.Thermostat thermostat in thermostats)
        {
            System.Collections.Hashtable hash=new System.Collections.Hashtable();
            hash.Add("name", thermostat.DisplayName);
            hash.Add("ipAddress", thermostat.IpAddress);
            hash.Add("brand", thermostat.Brand);
            al.Add(hash);
        }

        Hashtable result = new Hashtable();
        result.Add("thermostats", al);
        result.Add("zipCode", location.ZipCode);

        Response.Write(ThermostatMonitorLib.JSON.JsonEncode(result));
        Response.End();
    }

    private void LogWeather(int locationId)
    {
        int temperature = Convert.ToInt32(Request["temperature"]);
        ThermostatMonitorLib.OutsideCondition.CheckAndLog(locationId, temperature);
    }

    private void SetTemperature(int locationId)
    {
        string thermostatIp = Request["thermostatIP"];
        int temperature = Convert.ToInt32(Request["degrees"]);
        bool hold = Request["hold"] == "1";
        Commands.SetTemperature(locationId, thermostatIp, temperature, hold);
    }

    private void LogTemperature(int locationId)
    {
        string thermostatIp = Request["thermostatIP"];
        System.Int16 precision = Convert.ToInt16(Request["Precision"]);
        if (Request["Precision"] == null) precision = -1;

        ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(locationId, thermostatIp);
        if (thermostat == null) return;

        int temperature = Convert.ToInt32(Request["temperature"]);
        ThermostatMonitorLib.Temperature.CheckAndLogTemperature(thermostat.Id, temperature, precision);
    }


    private void LogCycle(bool start, int locationId)
    {
        string thermostatIp = Request["thermostatIP"];
        System.Int16 precision = Convert.ToInt16(Request["Precision"]);
        if (Request["Precision"] == null) precision = -1;

        ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(locationId, thermostatIp);
        if (thermostat == null) return;

        string mode = "";
        bool running = false;
        ThermostatMonitorLib.Cycle.EndOpenCycle(thermostat.Id, precision); //whether start or end, always close any open cycles

        if (start)
        {
            string cycleType = Request["cycleType"];
            ThermostatMonitorLib.Cycle.StartCycle(thermostat.Id, cycleType, precision);
        }
    }

}
