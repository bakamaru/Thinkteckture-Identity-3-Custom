setTimeout(function() {
    window.logout();
}, 3000);

setInterval(function() {
    var html = document.getElementsByTagName('h1')[0];
    var text = html.innerHTML;
    if (text.indexOf('now logged out') > -1) {
        window.close();
    }
}, 1000);