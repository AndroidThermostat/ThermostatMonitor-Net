//var apiUrl = 'http://api.thermostatmonitor.com/json/';
var apiUrl = 'http://localhost:401/json/';
var v2Url = 'http://localhost:401/v2/';

var apiKey = '';
var thermostats;
var weather;
function init() { Display.initTray(); Control.login(); }

//**********************CONTROL**********************
function Control() { }

Control.startTimer = function() { Control.updateThermostats(); setInterval('Control.updateThermostats()', 60000); }
Control.publishResults = function() { Data.checkin(apiKey); Display.renderThermostats(thermostats); }
Control.saveApiKey = function() { apiKey = $('#apiKeyText').val(); Titanium.App.Properties.setString('apiKey', apiKey); Control.login(); }

Control.login = function() {
    apiKey = Titanium.App.Properties.getString('apiKey', '');
    thermostats = Thermostat.loadThermostats(apiKey);
    if (thermostats.length > 0) Control.startMonitoring(); else Display.drawApiKeyScreen();
}

Control.updateThermostats = function() {
    for (i = 0; i < thermostats.length; i++) {
        switch (thermostats[i].brand) {
            case 'RTCOA': RadioThermostat.update(thermostats[i]); break;
            //case 'RCSTechnology': rcsUpdate(thermostats[i]); break;
        }
    }
    setTimeout('Control.publishResults()', 2000);
}

Control.startMonitoring = function() {
    weather = new Weather();
    $('#contentHolder').html('<div id="panels"></div>');
    Display.renderThermostats(thermostats);
    Control.startTimer() 
}




//**********************DATA**********************
function Data() { }
Data.checkin = function(apiKey) {
    var result = new Array(); var thermostatArray = []; var requestObj = {}; var url = apiUrl + '?apiKey=' + apiKey + '&action=checkin';
    for (i = 0; i < thermostats.length; i++) {
        var thermostat = thermostats[i];
        if (thermostat.successfulUpdate) {
            var t = {};
            t.state = thermostat.state;
            t.temperature = thermostat.temperature;
            t.ipAddress = thermostat.ipAddress;
            thermostatArray.push(t);
        }
    }
    requestObj.thermostats = thermostatArray;
    var jsonRequest = JSON.stringify(requestObj);
    $.ajax({
        url: url, data: jsonRequest, type: 'POST', async: false,
        success: function(data) {
            var response = JSON.parse(data); weather.loadJson(response);
            var commands = Command.loadCommandsJson(response); if (commands.length > 0) Command.runCommands(commands);
        }
    });
    return result;
}

function Thermostat() {
    this.name = '';
    this.ipAddress = '';
    this.brand = '';
    this.lastUpdated = new Date();
    this.temperature = -999;
    this.state = '';
    this.successfulUpdate = false;

    this.loadJson = function(t) {
        this.ipAddress = t['ipAddress'];
        this.name = t['name'];
        this.brand = t['brand'];
    };
}

Thermostat.loadThermostats = function(apiKey) {
    var result = new Array(); var url = apiUrl + '?apiKey=' + apiKey + '&action=loadSettings';
    $.ajax({
        url: url, async: false,
        success: function(data) {
            var settings = JSON.parse(data);
            for (i = 0; i < settings['thermostats'].length; i++) {
                var thermo = new Thermostat();
                thermo.loadJson(settings['thermostats'][i]);
                result.push(thermo);
            }
        }
    });
    return result;
}

function Weather() {
    this.zip = '';
    this.lastUpdated = new Date();
    this.temperature = -999;
    this.loadJson = function(w) {
        this.temperature = w['outsideTemperature'];
        this.zip = w['zip'];
        this.lastUpdated = new Date();
    };
}

function Command() {
    this.commandName = '';
    this.commandData = [];
    this.thermostatIP = '';
}

Command.loadCommandsJson = function(j) {
    var commands = j['commands'];
    if (commands == null) return [];
    var result = [];
    for (i = 0; i < commands.length; i++) {
        var c = commands[i];
        var command = new Command();
        command.commandName = c['commandName'];
        command.commandData = c['commandData'];
        command.thermostatIP = c['thermostatIP'];
        result.push(command);
    }
    
    return result;
}

Command.runCommands = function(commands) {for (i = 0; i < commands.length; i++) {Command.runCommand(commands[i]);}}

Command.runCommand = function(command) {
    switch (command.commandName) {
        case 'setTemperature':
            var degrees = command.commandData['degrees'];
            var hold = command.commandData['hold'];
            var url = 'http://' + command.thermostatIP + '/tstat';
            var requestObj = {};
            requestObj['t_cool'] = degrees;
            var jsonRequest = JSON.stringify(requestObj);
            $.ajax({url: url,data: jsonRequest,type: 'POST'});
            break;
    }
}






//**********************DISPLAY**********************
var _tray = null;
function Display() {}

Display.drawApiKeyScreen = function() { $('#contentHolder').html('<div class="apiKey"><b>API key:</b> <input type="text" id="apiKeyText" /><input type="button" value="Login" onclick="Control.saveApiKey();" /></div>'); Display.setWindowHeight(); }
Display.setWindowHeight = function() { Titanium.UI.getCurrentWindow().setHeight($('#contentHolder').height()); }
Display.initTray = function() {
    if (!Titanium.getPlatform() == 'win32') return;
    var window = Titanium.UI.getCurrentWindow();
    window.addEventListener(Titanium.MINIMIZED, Display.minimizeToTray);
}

Display.minimizeToTray = function() {
    var window = Titanium.UI.getCurrentWindow();window.hide();
    if (_tray == null) _tray = Titanium.UI.addTray("app://logo.png", function() {window.show();window.unminimize();}, false);
}

Display.renderThermostats = function(thermostats) {
    var panels = []; panels.push(Display.drawWeather());
    for (i = 0; i < thermostats.length; i++) { panels.push(Display.drawPanel(thermostats[i])); }
    $('#panels').html(panels.join('')); Display.setWindowHeight();
}

Display.drawWeather = function() {
    var result = [];
    result.push('<div class="panel">');
    result.push('  <div class="right">');
    if (weather.temperature == -999) {
        result.push('    <div class="temperature"></div>');
        result.push('    <div class="state">Loading...</div>');
    } else {
        result.push('    <div class="temperature">' + weather.temperature.toString() + '°</div>');
        result.push('    <div class="state"></div>');
    }
    result.push('  </div>');
    result.push('  <div class="name">Outside</div>');
    result.push('  <div class="ip">' + weather.zip + '</div>');
    result.push('  <div class="updated">Last Updated: ' + Display.formatTime(weather.lastUpdated) + '</div>');
    result.push('</div>');
    return result.join('\n');
}

/*
Display.drawRCS = function(t) {
    var result = [];ipParts = t.ipAddress.split('.');var divName='status' + ipParts[ipParts.length-1];
    result.push('<div class="panel">');
    result.push('  <div class="name">' + t.name + '</div>');
    result.push('  <div class="ip">' + t.ipAddress + '</div>');
    result.push('  <div id="' + divName + '"><a href="#" onclick="RCSTechnology.configure(\'' + t.ipAddress + '\', \'' + divName + '\');">Sync</a></div>');
    result.push('</div>');
    return result.join('\n');
}
*/

Display.drawPanel = function(t) {
//if (thermostats[i].brand != 'RTCOA') return Display.drawRCS(t);
    if (thermostats[i].brand != 'RTCOA') return [];
    var result = [];
    result.push('<div class="panel">');
    result.push('  <div class="right">');
    if (t.temperature == -999) {
        result.push('    <div class="temperature"></div>');
    } else {
        result.push('    <div class="temperature">' + t.temperature.toString() + '°</div>');
    }
    result.push('    <div class="state">' + t.state + '</div>');
    result.push('  </div>');
    result.push('  <div class="name">' + t.name + '</div>');
    result.push('  <div class="ip">' + t.ipAddress + '</div>');
    result.push('  <div class="updated">Last Updated: ' + Display.formatTime(t.lastUpdated) + '</div>');
    result.push('</div>');
    return result.join('\n');
}

Display.formatTime = function(time) {
    var hour = time.getHours(); var minute = time.getMinutes().toString(); var amPm = 'am';
    if (time.getMinutes() < 10) minute = '0' + minute;
    if (time.getHours() > 11) {amPm = 'pm';hour = hour - 12;}
    if (hour == 0) hour = 12; return hour.toString() + ':' + minute + ' ' + amPm;
}




//**********************RADIO THERMOSTAT**********************
function RadioThermostat() { }

RadioThermostat.update = function(t) {
    var url = 'http://' + t.ipAddress + '/tstat';t.successfulUpdate = false;
    $.ajax({
        url: url,
        success: function(data) {
            t.temperature = data['temp'];
            switch (data['tstate']) {
                case 0: t.state = 'Off'; break;
                case 1: t.state = 'Heat'; break;
                case 2: t.state = 'Cool'; break;
            }
            t.lastUpdated = new Date(); t.successfulUpdate = true;
        },
        async: false
    });
}




//**********************RCSTechnology**********************
/*
function RCSTechnology() { }
RCSTechnology.configure = function(ipAddress, statusDivName) {
    var statusDiv = $('#' + statusDivName); statusDiv.text('configuring'); var isEcho=false; var step = "commandMode";
    var socket = Titanium.Network.createTCPSocket(ipAddress, 2000);
    socket.onRead(function(data) {
        if (isEcho)
        {
            isEcho=false;  //while in command mode, data sent is echoed back and we need to ignore every other response.
        } else {
            isEcho = true; statusDiv.text(step);
            switch (step) {
                case "commandMode": socket.write('\$$$\r'); step="setHost"; isEcho=false; break;
                case "setHost": socket.write('set ip host 0\r'); step="setDNS"; break;
                case "setDNS": socket.write('set d n api.thermostatmonitor.com\r'); step="setPort"; break;
                case "setPort": socket.write('set ip remote 2000\r'); step="setPeriod"; break;
                case "setPeriod": socket.write('set sys autoconn 60\r'); step="setIdle"; break;
                case "setIdle": socket.write('set comm idle 5\r'); step="setApiKey"; break;
                case "setApiKey": socket.write('set comm remote ' + apiKey.replace(/-/g,'') + '\r'); step="save"; break;
                case "save": socket.write('save\r'); step="reboot"; break;
                case "reboot": socket.write('reboot\r'); step="Successfully configured"; socket.close(); statusDiv.text(step); break;
            }
        }
    });
    socket.connect();
}
*/