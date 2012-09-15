function initTray() {
    var window = Titanium.UI.getCurrentWindow();
    Titanium.UI.getCurrentWindow().addEventListener(Titanium.MINIMIZED, function(event) {
        window.hide();
        tray = Titanium.UI.addTray("app://logo.png", function(evt) {
            if (evt.getType() == 'clicked') {
                Titanium.UI.clearTray();
                window.show();
                window.unminimize();
            }
        });
    });
}


function drawApiKeyScreen() {
    $('#contentHolder').html('<div class="apiKey"><b>API key:</b> <input type="text" id="apiKeyText" /><input type="button" value="Login" onclick="saveApiKey();" /></div>');
    setWindowHeight();
}

function setWindowHeight() {
    Titanium.UI.getCurrentWindow().setHeight($('#contentHolder').height());
}

function renderThermostats(thermostats) {
    var panels = [];
    panels.push(drawWeather());
    for (i = 0; i < thermostats.length; i++) {
        panels.push(drawPanel(thermostats[i]));
    }
    $('#panels').html(panels.join(''));
    setWindowHeight();
}

function drawWeather() {
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
    result.push('  <div class="updated">Last Updated: ' + formatTime(weather.lastUpdated) + '</div>');
    result.push('</div>');
    return result.join('\n');
}

function drawPanel(t) {
    var result=[];
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
    result.push('  <div class="updated">Last Updated: ' + formatTime(t.lastUpdated) + '</div>');
    result.push('</div>');
    return result.join('\n');
}

function formatTime(time) {
    var hour = time.getHours();
    var minute = time.getMinutes().toString();
    var amPm = 'am';

    if (time.getMinutes() < 10) minute = '0' + minute;
    if (time.getHours() > 11) {
        amPm = 'pm';
        hour = hour - 12;
    }
    if (hour == 0) hour = 12;
    return hour.toString() + ':' + minute + ' ' + amPm;
}