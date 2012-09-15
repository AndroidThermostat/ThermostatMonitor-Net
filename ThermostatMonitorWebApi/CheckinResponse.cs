using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThermostatMonitorWebApi;
using System.Collections;
using System.Collections.Generic;

namespace ThermostatMonitorWebApi
{
    public class CheckinResponse
    {

        private static Hashtable _conditionHash = new Hashtable();
        public static Hashtable ConditionHash
        {
            get { return _conditionHash; }
        }

        public Commands ReplyCommands;
        public int OutsideTemperature;
        public string ZipCode;
    
        public string GetJson()
        {
            System.Collections.Hashtable result = new Hashtable();
            if (ReplyCommands!=null && ReplyCommands.Count > 0)
            {
                System.Collections.ArrayList al = new ArrayList();
                foreach (Command command in ReplyCommands)
                {
                    Hashtable c = new Hashtable();
                    c["commandName"] = command.CommandName;
                    c["thermostatIP"] = command.ThermosatIP;
                    c["commandData"] = command.CommandData;
                    al.Add(c);
                }
                result.Add("commands", al);
            }
            result.Add("outsideTemperature", this.OutsideTemperature.ToString());
            result.Add("zip", this.ZipCode);
            return ThermostatMonitorLib.JSON.JsonEncode(result);
        }
    }

}
