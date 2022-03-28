var http = require('http');
var express = require('express');
var app = express();

app.set('view engine', 'ejs');
app.set('views', './views');

app.use(express.urlencoded({extended: true }));

app.use(express.static('./static'));

var router = require('./router.js');
app.use('/', router);

// Default path if it's not specified in router
app.use((req,res,next) => {
    res.render("404", {url: req.url});
    res.end();
});

app.disable('etag');

http.createServer(app).listen(8080);

console.log("Server started...AAA");

