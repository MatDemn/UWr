var http = require('http');
var express = require('express');
var app = express();
var multer = require('multer');

var uploadObj = multer({ dest: 'uploads/' });
app.set('view engine', 'ejs');
app.set('views', './views');

app.use(express.urlencoded({extended: true }));
app.use('/uploads', express.static('uploads'));

app.get('/', (req,res) => {
    res.render('fileUpload');
});

app.post('/uploaded', uploadObj.single('filename'), (req, res, next) => {
    res.render('uploaded');
});

http.createServer(app).listen(8080);

console.log("Server started...");
