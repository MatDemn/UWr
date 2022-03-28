var http = require('http');

var server = 
    http.createServer(
    (req, res) => {
        res.setHeader('Content-type', 'text/html; charset=utf-8');
        res.end(`hello world ${new Date()}, ${req.url}`);
}).listen(8888);

console.log("Server started...");