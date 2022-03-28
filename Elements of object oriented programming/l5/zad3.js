var http = require('http');
var fileName = 'result.txt';

var server = 
    http.createServer(
    (req, res) => {
        res.setHeader('Content-Disposition', 'attachment;filename='+ fileName);
        res.write('File text is here!');
        res.end();
}).listen(8888);

console.log("Server started...");