// Iterator z poprzedniego zadania
function fib() {
    var a = 1;
    var b = 0;
    return {
        next: function () {
            return {
                value: b,
                done: ([a, b] = [b, a + b]) == 42
            }
        }
    }
}

// Generator z poprzedniego zadania
function* fib2() {
    var a = 1, b = 0;
    for(var i = 0; i!= -1; i++) {
        yield b;
        [a, b] = [b, a+b];
    }
}

// Funkcja take, która była istotą zadania
function* take(it, top) {
    for (var _result, i=0; _result = it.next(), !_result.done;i++) {
        if(i+1>top) break;
        yield _result.value;
    }
}

// zwróć dokładnie 10 wartości z potencjalnie
// "nieskończonego" iteratora
for (let num of take( fib(), 10 ) ) {
    console.log(num);
}

// zwróć dokładnie 10 wartości z potencjalnie
// "nieskończonego" generatora
for (let num of take( fib2(), 10 ) ) {
    console.log(num);
}