/* 
    Korzystam z wiedzy z wykładu. Nałożyłem po prostu funkcję bezargumentową na wewnętrzny kod
    i taką funkcję zwracam w createGenerator. Już działa iteracja do n.
*/

function createGenerator(n) {
    return function () {
        var _state = 0;

        return {
            next: function () {
                return {
                    value: _state,
                    done: _state++ >= n
                }
            }
        }
    }
}

var foo = {
    [Symbol.iterator]: createGenerator(10)
};

var bar = {
    [Symbol.iterator]: createGenerator(5)
};

var foobar = {
    [Symbol.iterator]: createGenerator(42)
};

console.log("Pierwszy obiekt");

for (var f of foo)
    console.log(f);

console.log("Drugi obiekt");

for (var f of bar)
    console.log(f);

console.log("Trzeci obiekt");

for (var f of foobar)
    console.log(f);

