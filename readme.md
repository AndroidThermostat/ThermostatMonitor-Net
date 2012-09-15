#Thermostat Monitor

This repository contains the source code for the website ThermostatMonitor.com, a toold for tracking the usage of WiFi enabled thermostats.

## License

Copyright (C) 2012 Trilitech, LLC

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Components

* Database - Contains a SQL script file for creating the MS SQL database where usage and configuration data is stored.

* ThermostatMonitor - Contains a very small <a href="http://www.appcelerator.com/">Titanium Appcelerator</a> application that runs on users desktops to relay information from RadioThermostat devices.

* ThermostatMonitorLib - Contains data access code and logic that is used by ThermostatMonitorWeb and ThermostatMonitorWebApi

* ThermostatMonitorWeb - The ASP.NET website code behind ThermostatMonitor.com

* ThermostatMonitorWebAPI - The ASP.NET website code behind api.thermostatmonitor.com.  The client applications call this api to store usage data.