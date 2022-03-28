var http = require("http");
var express = require("express");
var app = express();

app.use(express.urlencoded({extended: true}));

app.set('view engine', 'pug');
app.set('views', './views');

app.get('/', (req, res) => {
    res.render('index');
});


app.post('/', (req, res) => {
    var imie = req.body.imie;
    var nazwisko = req.body.nazwisko;
    var przedmiot = req.body.przedmiot;
    var zad = req.body.zad;

    if(validateData(imie, nazwisko, przedmiot, zad)) {
        res.render('print', {imie: imie, nazwisko: nazwisko, przedmiot: przedmiot, zad: zad});
    }
    else{
        res.render('index', {errorMsg: "Wprowadź poprawnie wszystkie dane, następnie wyślij formularz..."});
    }
});

function validateData(imie, nazwisko, przedmiot, zad) {
    for(var i = 0; i<10; i++) {
        if(zad[i] == "")
            zad[i] = 0; 
    }
    return imie!=""&&nazwisko!=""&&przedmiot!="";
}

http.createServer(app).listen(8888);