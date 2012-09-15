using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using ThermostatMonitorWebApi;
using System.IO;

namespace ThermostatMonitorWebApi.JSON
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ThermostatMonitorLib.Location location = null;
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
                case "loadsettings":
                    LoadSettings(location);
                    break;
                case "checkin":
                    CheckIn(location);
                    break;
                case "settemperature":
                    SetTemperature(location.Id);
                    break;
            }
        }

        private void SetTemperature(int locationId)
        {
            string thermostatIp = Request["thermostatIP"];
            int temperature = Convert.ToInt32(Request["degrees"]);
            bool hold = Request["hold"] == "1";
            Commands.SetTemperature(locationId, thermostatIp, temperature, hold);
        }

        private void CheckIn(ThermostatMonitorLib.Location location)
        {
            StreamReader reader = new StreamReader(Request.InputStream, Request.ContentEncoding);
            string jsonInput = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            CheckinRequest request = CheckinRequest.Load(location, jsonInput);
            CheckinResponse response = new CheckinResponse();

            //Update inside temperature and thermostat state
            foreach (Thermostat thermostat in request.Thermostats)
            {
                string key=location.Id.ToString() + "_" + thermostat.IpAddress;
                Thermostat previousThermostat = new Thermostat();
                if (Thermostats.Cache.ContainsKey(key)) previousThermostat = (Thermostat)Thermostats.Cache[key];
                if (thermostat.Temperature != previousThermostat.Temperature) LogTemperatureChange(location.Id, thermostat, previousThermostat);
                if (thermostat.State != previousThermostat.State) LogStateChange(location.Id, thermostat, previousThermostat);
                Thermostats.Cache[key] = thermostat;
            }

            //Update outside temperature
            response.ZipCode = request.ZipCode;


            string cacheKey = "OutsideTemperature" + location.Id.ToString();
            if (Cache.Get(cacheKey)==null)
            {
                try
                {
                    response.OutsideTemperature = ThermostatMonitorLib.Weather.GetTemperature(location.OpenWeatherCityId);
                    Cache.Add(cacheKey, response.OutsideTemperature, null, DateTime.Now.AddMinutes(4).AddSeconds(30), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                } catch{}
            } else {
                response.OutsideTemperature = Convert.ToInt32(Cache.Get(cacheKey));
            }

            
            int previousTemperature = -999;
            if (CheckinResponse.ConditionHash.ContainsKey(location.Id)) previousTemperature = Convert.ToInt32(CheckinResponse.ConditionHash[location.Id]);

            if (response.OutsideTemperature != previousTemperature  && response.OutsideTemperature!=0) ThermostatMonitorLib.OutsideCondition.Log(location.Id, response.OutsideTemperature);
            CheckinResponse.ConditionHash[location.Id] = response.OutsideTemperature;
            
            //Add any pending commands to the response
            response.ReplyCommands = Commands.Get(location.Id);
            Commands.Remove(location.Id);

            try
            {
                Thermostats.CleanCache();
            }
            catch (Exception ex)
            {
                ThermostatMonitorLib.Error error = new ThermostatMonitorLib.Error();
                error.ErrorMessage = ex.ToString();
                error.LogDate = DateTime.Now;
                error.UserId = 0;
                error.Url = "/json/default.aspx";
                ThermostatMonitorLib.Error.SaveError(error);
            }
            
            Response.Write(response.GetJson());
            Response.End();
        }


        private void LogTemperatureChange(int locationId, Thermostat current, Thermostat previous)
        {
            ThermostatMonitorLib.Thermostat thermostat=ThermostatMonitorLib.Thermostat.LoadThermostat(locationId, current.IpAddress);
            double precision = System.Math.Round(new TimeSpan(DateTime.Now.Ticks - previous.CheckinTime.Ticks).TotalMinutes, 0);
            if (precision > 100) precision = -1;
            ThermostatMonitorLib.Temperature.LogTemperature(thermostat.Id, current.Temperature, (short)precision);
        }

        private void LogStateChange(int locationId, Thermostat current, Thermostat previous)
        {
            ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(locationId, current.IpAddress);
            double precision = System.Math.Round(new TimeSpan(DateTime.Now.Ticks - previous.CheckinTime.Ticks).TotalMinutes, 0);
            if (precision > 100) precision = -1;

            ThermostatMonitorLib.Cycle.EndOpenCycle(thermostat.Id, (short)precision);
            if (current.State != "Off")
            {
                ThermostatMonitorLib.Cycle.StartCycle(thermostat.Id, current.State, (short)precision);
            }
            else
            {
                ThermostatMonitorLib.Snapshots.Generate(thermostat.Id);
            }
        }



        private void LoadSettings(ThermostatMonitorLib.Location location)
        {
            //Build array list of thermostats
            ThermostatMonitorLib.Thermostats thermostats = ThermostatMonitorLib.Thermostats.LoadThermostatsByLocationId(location.Id);
            ArrayList al = new ArrayList();
            foreach (ThermostatMonitorLib.Thermostat thermostat in thermostats)
            {
                System.Collections.Hashtable hash = new System.Collections.Hashtable();
                hash.Add("name", thermostat.DisplayName);
                hash.Add("ipAddress", thermostat.IpAddress);
                hash.Add("brand", thermostat.Brand);

                ThermostatMonitorLib.Cycle c=ThermostatMonitorLib.Cycle.LoadOpenCycle(thermostat.Id);
                if (c == null) hash.Add("state", "Off"); else hash.Add("state",c.CycleType);

                ThermostatMonitorLib.Temperature t = ThermostatMonitorLib.Temperature.LoadCurrentTemperature(thermostat.Id);
                if (t != null) hash.Add("temperature", t.Degrees);

                al.Add(hash);
            }
            Hashtable result = new Hashtable();
            result.Add("thermostats", al);
            string output = ThermostatMonitorLib.JSON.JsonEncode(result);
            OutputLit.Text = output;
        }


    }
}
