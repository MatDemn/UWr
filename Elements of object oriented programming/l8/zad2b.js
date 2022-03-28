var http = require('http');
var express = require('express');
var sql = require('mssql/msnodesqlv8');
var app = express();

app.set('view engine', 'ejs');
app.set('views', './views');

app.use(express.urlencoded({extended: true }));

app.use(express.static('./static'));

app.disable('etag');

const pool = new sql.ConnectionPool({
  database: 'WEPPO_8',
  server: "(localdb)\\MSSQLLocalDB",
  driver: 'msnodesqlv8',
  options: {
    trustedConnection: true
  }
});

// Home page route.
app.get('/', function (req, res) {
    res.render("index");
  })

app.post('/result', function(req, res) {
    pool.connect();
    pool.request().query(
      `INSERT INTO dbo.Osoba (Imie, Nazwisko, Plec, MiejsceZamieszkania, Telefon) \
VALUES ('${req.body['Imie']}', '${req.body['Nazwisko']}', '${req.body['Plec']}', '${req.body['MiejsceZamieszkania']}', '${req.body['Telefon']}')`,
      (err,results) => {
      if(err) res.send(err);
      else {console.dir(results);}
      
    });
    //pool.request.query("SELECT SCOPE_IDENTITY()");

    res.render('result', 
      {fields: req.body});
    //res.redirect('/');
});


http.createServer(app).listen(8080);

console.log("Server started...AAA");

