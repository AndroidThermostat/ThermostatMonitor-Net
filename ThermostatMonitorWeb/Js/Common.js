function login(emailVar, passwordVar) {
    $.post('/ajax/login.aspx', { email: emailVar, password: passwordVar }, function(data) {
        location.href = '/cp/';
    });
}

function detectLoginEnter(event) {
    if (event.which == 13) {
        event.preventDefault();
        login($('#loginEmail').val(), $('#loginPassword').val());
    }
}

function initLoginLinks() {
    $('#loginEmail').keypress(function(event) { detectLoginEnter(event) });
    $('#loginPassword').keypress(function(event) { detectLoginEnter(event) });
}

$(document).ready(function() {
    initLoginLinks();
});