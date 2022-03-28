function fib1() {
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

function* fib2() {
    var a = 1, b = 0;
    for(var i = 0; i!= -1; i++) {
        yield b;
        [a, b] = [b, a+b];
    }
}

var _it = fib1();

for (var _result; _result = _it.next(), !_result.done;) {
    console.log(_result.value);

    if(_result.value> 1000) break;
}

// To zwraca błąd
/* 
for(var i of _it) {
    console.log(i);
    if(i > 1000) break;
}*/

var it = fib2();
for(var i of it) {
    console.log(i);
    if(i > 1000) break;
}
/* Czyli możemy się iterować za pomocą for of w tym drugim przypadku. */
