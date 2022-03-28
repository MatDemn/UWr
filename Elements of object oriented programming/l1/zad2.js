///         ZADANIE 2

///         Funkcja sprawdzająca czy liczba dzieli się przez swoje cyfry składowe
function divideByDigits(number) {
    var copyNumber = number;
    while(copyNumber != 0) {
        var temp = copyNumber % 10;
        copyNumber /= 10;
        copyNumber = Math.floor(copyNumber);
        if(number % temp != 0) 
            return false;
    }
    return true;
}

///         Funkcja sprawdzająca czy liczba dzieli się przez sumę swoich cyfr składowych
function divideBySumDigits(number) {
    var copyNumber = number;
    var sumDigits = 0;
    while(copyNumber != 0) {
        var temp = copyNumber % 10;
        copyNumber /= 10;
        copyNumber = Math.floor(copyNumber);
        sumDigits += temp;
    }
    return number % sumDigits == 0;

}

///         Funkcja wyszukująca w przedziale [1; maxVal] 
///         liczby dzielące się przez sumę swoich cyfr składowych oraz każdą cyfrę skłądową z osobna
function divideCheck(maxVal) {
    for(var i = 1; i <= maxVal; i++) {
        if(divideByDigits(i) && divideBySumDigits(i))
            console.log(i);
    }
}

console.log("Zadanie 2:");
divideCheck(100000);