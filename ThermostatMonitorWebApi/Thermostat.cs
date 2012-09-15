using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThermostatMonitorWebApi
{
    public class Thermostat
    {
        public int Temperature;
        public string State;
        public DateTime CheckinTime;
        public string IpAddress;
    }
}
