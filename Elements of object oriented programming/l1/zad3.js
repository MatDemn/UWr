///         ZADANIE 3

///         Test na "pierwszość" liczby. Wystarczy sprawdzić podzielność aż do pierwiastka kwadratowego z tej liczby włącznie
function isPrime(number) {
    for(var i = 2; i <= Math.sqrt(number); i++) {
        if(number % i == 0) return false;
    }
    return true;
}

function findPrimes(maxVal) {
    for(var i = 2; i<=maxVal; i++) {
        if(isPrime(i))
            console.log(i);
    }
}

console.log("Zadanie 3:"); 
findPrimes(100000);