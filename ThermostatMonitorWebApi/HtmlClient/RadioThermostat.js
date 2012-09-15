function radioThermostatUpdate(t) {
    var url = 'http://' + t.ipAddress + '/tstat';
    t.successfulUpdate = false;
    $.ajax({
        url: url,
        success: function(data) {
            t.temperature = data['temp'];
            switch (data['tstate']) {
                case 0:
                    t.state = 'Off';
                    break;
                case 1:
                    t.state = 'Heat';
                    break;
                case 2:
                    t.state = 'Cool';
                    break;
            }
            t.lastUpdated = new Date();
            t.successfulUpdate = true;
        },
        async: false
    });
}
