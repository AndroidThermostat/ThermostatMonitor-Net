
function Thermostat() {
    this.name = '';
    this.ipAddress = '';
    this.brand='';
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

function loadSettings(apiKey) {
    var result = new Array();
    var url = apiUrl + '?apiKey=' + apiKey + '&action=loadSettings';
    $.ajax({
        url: url,
        success: function(data) {
            var settings = JSON.parse(data);
            for (i = 0; i < settings['thermostats'].length; i++) {
                var thermo = new Thermostat();
                thermo.loadJson(settings['thermostats'][i]);
                result.push(thermo);
            }
        },
        async: false
    });
    return result;
}

function checkin(apiKey) {
    var result = new Array();
    var url = apiUrl + '?apiKey=' + apiKey + '&action=checkin';

    var thermostatArray = [];
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
    var requestObj = {};
    requestObj.thermostats = thermostatArray;
    var jsonRequest = JSON.stringify(requestObj);

    $.ajax({
        url: url,
        data: jsonRequest,
        success: function(data) {
            var response = JSON.parse(data);
            weather.loadJson(response);
        },
        type: 'POST',
        async: false
    });
    return result;
}