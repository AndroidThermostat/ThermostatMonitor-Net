using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Generic;

namespace ThermostatMonitorWebApi
{
    public class Commands:List<Command>
    {
        public static Hashtable PendingCommands = new Hashtable();

        public static Commands Get(int locationId)
        {
            if (!PendingCommands.Contains(locationId)) return null; else return (Commands)PendingCommands[locationId];
        }

        public static void Add(int locationId, Command c)
        {
            Commands commands = new Commands();
            if (PendingCommands.Contains(locationId)) commands = (Commands)PendingCommands[locationId];
            commands.Add(c);
            PendingCommands[locationId] = commands;
        }

        public static void Remove(int userId)
        {
            PendingCommands.Remove(userId);
        }


        public static void SetTemperature(int locationId, string thermostatIP, int degrees, bool hold)
        {
            Command c = new Command();
            c.CommandName = "setTemperature";
            c.ThermosatIP = thermostatIP;
            c.CommandData = new Hashtable();
            c.CommandData.Add("degrees", degrees);
            c.CommandData.Add("hold", hold);
            Commands.Add(locationId, c);
        }
    }
}
