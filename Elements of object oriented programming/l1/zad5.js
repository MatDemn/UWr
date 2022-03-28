///         ZADANIE 5
/// Wyjaśnienie działania (F(n) to n-ta liczba ciągu fibonacciego) iteracyjnego kodu:
/// fibn1   fibn2    |  fibn1   fibn2   |   fibn1   fibn2       |    fibn1   fibn2
/// F(n)    F(n+1)  ->  F(n+1)  F(n)    ->  F(n+1)  F(n)+F(n+1) ->  F(n+1)  F(n+2)
/// Stąd widzimy, że taka metoda działa.

/*      
        Puszczenie rekurencyjnej funkcji liczącej n-tą liczbę fibonacciego dla argumentu 45 
        trwało u mnie ~11 sekund (iteracyjna wersja wykonała się w mgnieniu oka). 
        Ten algorytm rekurencyjny ma złożoność wykładniczą, więc każda niewielka zmiana n będzie się wziązała
        z gigantyczną zmianą czasu wykonania programu. Nie ma sensu w ten sposób liczyć większych wartości.
*/

function iterFib(n) {
    var fibn1 = 1;
    var fibn2 = 1;
    for(var i = 2; i <= n; i++) {
        [fibn1, fibn2] = [fibn2, fibn1];
        fibn2 += fibn1;
    }
    return fibn2;
}

function recFib(n) {
    if(n == 0 || n == 1) return 1;
    else
        return recFib(n-1) + recFib(n-2);
}

const timerLabel1 = "Czas wykonania rekurencyjnej wersji dla n = 45";
const timerLabel2 = "Czas wykonania iteracyjnej wersji dla n = 45";

console.log("Zadanie 5:");

console.time(timerLabel2);
iterFib(45);
console.timeEnd(timerLabel2);

console.time(timerLabel1);
recFib(45);
console.timeEnd(timerLabel1);