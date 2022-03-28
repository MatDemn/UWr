function createFs(n) { // tworzy tablicę n funkcji
    var fs = []; // i-ta funkcja z tablicy ma zwrócić i
    for (var i = 0; i < n; i++) {
        fs[i] =
            function () {
                return i;
            };
    };
    return fs;
}
var myfs = createFs(10);
console.log(myfs[0]()); // zerowa funkcja miała zwrócić 0
console.log(myfs[2]()); // druga miała zwrócić 2
console.log(myfs[7]());
// 10 10 10

/* 
    Var ma zasięg ograniczony do najbliższej funkcji, w której jest umieszczony. 
    W takim razie na końcu pętli i ma wartość 10, więc ta 10 pojawia się w każdym elemencie tablicy.
    Let ma zasięg ograniczony tylko do danego bloku, więc tylko w pętli for.
*/

function createFsNew(n) { // tworzy tablicę n funkcji
    var fs = []; // i-ta funkcja z tablicy ma zwrócić i
    for (var i = 0; i < n; i++) {
        (function (i) {
            fs[i] =
                function () {
                    return i;
                };
        })(i);
    };
    return fs;
}
var myfs = createFsNew(10);
console.log(myfs[0]()); // zerowa funkcja miała zwrócić 0
console.log(myfs[2]()); // druga miała zwrócić 2
console.log(myfs[7]());
// 10 10 10

/* 
    Zamknięcie całego wyrażenia w nową funkcję i wywołanie jej z odpowiednim argumentem zapewnia już
    świeże i, różne od tego z pętli. Tak więc dla każdego obrotu pętli mamy świeże i, z odpowiednią wartością.

    Babel nie był niestety pokazany na wykładzie. Jednak wkleję tutaj kod wygenerowany
    przez kompilator i go opiszę:
*/


function createFs2(n) {
    // tworzy tablicę n funkcji
    var fs = []; // i-ta funkcja z tablicy ma zwrócić i

    var _loop = function _loop(i) {
        fs[i] = function () {
            return i;
        };
    };

    for (var i = 0; i < n; i++) {
        _loop(i);
    }

    ;
    return fs;
}

var myfs = createFs2(10);
console.log(myfs[0]()); // zerowa funkcja miała zwrócić 0
console.log(myfs[2]()); // druga miała zwrócić 2
console.log(myfs[7]());

/*
  Jak widać, Babel stworzył funkcję _loop, która przyjmuje argument i. Taka funkcja jest wywoływana
  przy każdej iteracji pętli for. Dzięki temu mamy za każdym razem "świeże" i przy każdej iteracji pętli.
*/