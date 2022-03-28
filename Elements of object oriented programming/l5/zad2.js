var https = require('https');
var fs = require('fs');

var server = 
    https.createServer(
        {
            key: fs.readFileSync("./certificate/key.pem"),
            cert: fs.readFileSync("./certificate/cert.pem")
        },
    (req, res) => {
        res.setHeader('Content-type', 'text/html; charset=utf-8');
        res.end(`hello world ${new Date()}, ${req.url}`);

        console.log(`${req.url}`);
}).listen(8888);

console.log("Server started...");