
function configureRCS(t) {
    var isEcho=false;
    var step="commandMode":
    var socket = Titanium.Network.createTCPSocket(t.ipAddress, 2000);
    socket.onRead(function(data) {
        if (isEcho)
        {
            isEcho=false;  //while in command mode, data sent is echoed back and we need to ignore every other response.
        } else {
            isEcho=true;
            switch (step)
            {
                case "commandMode":
                    socket.write('\$$$\r');
                    step="setHost";
                    isEcho=false;
                    break;
                case "setHost":
                    socket.write('set ip host 0\r');
                    step="setDNS";
                    break;
                case "setDNS":
                    socket.write('set d n api.thermostatmonitor.com\r');
                    step="setPort";
                    break;
                case "setPort":
                    socket.write('set ip remote 2000\r');
                    step="setPeriod";
                    break;
                case "setPeriod":
                    socket.write('set sys autoconn 60\r');
                    step="setIdle";
                    break;
                case "setIdle":
                    socket.write('set comm idle 5\r');
                    step="setApiKey";
                    break;
                case "setApiKey":
                    socket.write('set comm remote ' + apiKey.replace('-','') + '\r');
                    step="save";
                    break;
                case "save":
                    socket.write('save\r');
                    step="reboot";
                    break;
                case "reboot":
                    socket.write('reboot\r');
                    step="Successfully configured";
                    socket.close();
                    break;
            }
        }
    });
}

function rcsUpdate(t) {
    var socket = Titanium.Network.createTCPSocket(t.ipAddress, 2000);
    socket.onRead(function(data) {

        if (data == '*HELLO*') {
            socket.write('\rA=1 R=1\r');
        } else if (data.indexOf(' T') > -1) {
            var parts = data.split(' ');
            for (i = 0; i < parts.length; i++) {
                var part = parts[i];
                if (part.substring(0, 1) == 'T') t.temperature = part.replace('T=', '');
            }
            socket.write('\rA=1 R=2\r');
        } else {
            var parts = data.split(' ');
            var fan = false;
            for (i = 0; i < parts.length; i++) {
                var part = parts[i];
                switch (part) {
                    case 'FA=1':
                        fan = true;
                        break;
                    case 'H1A=1':
                        t.state = 'Heat';
                        break;
                    case 'C1A=1':
                        t.state = 'Cool';
                        break;
                }
            }
            if (fan && t.state=='Off') t.state='Fan';
            t.lastUpdated = new Date();
            t.successfulUpdate = true;
            socket.close();
        }

    });
    t.successfulUpdate = false;
    t.state = 'Off';
    socket.connect();
}