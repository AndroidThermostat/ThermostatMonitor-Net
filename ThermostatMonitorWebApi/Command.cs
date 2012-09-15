using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ThermostatMonitorWebApi
{
    public class Command
    {
        public string ThermosatIP;
        public string CommandName;
        public Hashtable CommandData;
    }
}
