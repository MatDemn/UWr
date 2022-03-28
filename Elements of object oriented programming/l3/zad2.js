/* Dwie poniższe funkcje zostały skopiowane z moich rozwiązań listy 1 */

function recFib(n) {
    if(n == 0 || n == 1) return 1;
    else
        return recFib(n-1) + recFib(n-2);
}

function memo(f) {

    var cache = {}

    return function(x) {

        if ( cache[x] === undefined ) {
            var f_x = f(x);
            cache[x] = f_x;
            return f_x;
        }
        else {
            return cache[x];
        }
    }
}

const timerLabel1 = "Czas wykonania rekurencyjnej wersji dla n = 45";
const timerLabel3 = "Czas wykonania rekurencyjnej wersji dla n = 45, z memoizacją";

console.time(timerLabel1);
recFib(45);
console.timeEnd(timerLabel1);

var recFib = memo(recFib);

console.time(timerLabel3);
recFib(45);
console.timeEnd(timerLabel3);

/* 
    Jak widzimy, czas poprawił się znacznie.
*/

