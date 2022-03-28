/*  

    Operator typeof(variable) zwraca nam typ zmiennej variable podaną w postaci stringa.
    Służy głównie do zsprawdzania typów prostych takich jak: 1, "abcdef", 1.5. 
    Nie nadaje się do obiektów, typów złożonych. Jeśli podamy jako argument obiekt,
    dostaniemy po prostu informację, że to jest zmienna typu "object",
    bez szczegółów.

    Operator variable instanceof class zwraca nam informację czy zmienna variable jest
    instancją klasy class. Służy do badania typów złożonych, obiektów.
    Typy proste zwracają tutaj wartość false, niezależnie czy podamy właściwą klasę po instanceof.

*/

console.log(typeof(1));
console.log(typeof("abcdef"));
console.log(typeof(1.5));
console.log(typeof({a: 1, b: "abc"}));

class Human {
    constructor(eyesColor, nation) {
        this.eyesColor = eyesColor;
        this.nation = nation;
    }

}

var hum1 = new Human("green", "Poland");
console.log("--------------------------");
console.log(hum1 instanceof Human);
console.log(hum1 instanceof Number);
// Tutaj będzie false
console.log(1 instanceof Number);
// Tutaj będzie false
console.log("abcdef" instanceof String);
