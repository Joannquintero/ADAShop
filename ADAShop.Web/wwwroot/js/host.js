/*
    HOST file to get the correct route when using AJAX to call a service
*/

var Host = {
    GetHostName: function () {
        var host = window.location.host,
            protocol = window.location.protocol;
        return (protocol + "//" + host);
    }
};