var human = {
    eyesColor:      'green',
    height:         174,
    weight:         64,
    nation:         'Poland',
    language:       'polish',
    '1':              'foo',
    1:            'bar'
}

var ex1 = {
    '1':              'foo',
    1:            'bar'
}

var ex2 = {
    1:              'foo',
    '1':            'bar'
}
/*

Użycie operatorów . oraz [] do odwoływania się do składowych obiektu.
– Jakie są różnice między tymi dwoma sposobami?

*/

console.log("Przykłady użycia 'dot notation' (notacji kropkowej):");
console.log(`Kolor oczu: \t${human.eyesColor}`);
console.log(`Wzrost: \t${human.height * 0.01}m`);
console.log(`Kraj:  \t\t${human.nation.substr(0, 3)}${human.nation.substr(3, 4)}`);
console.log(`Kraj: \t\t${human.language.toUpperCase()}`);

console.log("Przykłady użycia 'bracket notation' (notacji nawiasowej):");

var temp = 'eyesColor', temp1 = 'height', temp2 = 'nation', temp3 = 'language';
human['human mood'] = 'happy';

console.log(`Kolor oczu: \t${human[temp]}`);
console.log(`Wzrost: \t${human[temp1] * 0.01}m`);
console.log(`Kraj:  \t\t${human[temp2].substr(0, 3)}${human['nation'].substr(3, 4)}`);
console.log(`Kraj: \t\t${human[temp3].toUpperCase()}`);
console.log(`Humor: \t\t${human['human mood'].toUpperCase()}`);

/*


    Widzimy, że te notacje skutkują tym samym, czyli możemy się dostać do wartości.

    Różnice:
        Notacja kropkowa:
        -identyfikatory pól muszą być złożone ze znaków alfanumerycznych oraz _ i $
        -identyfikatory pól nie mogą się zaczynać od cyfr
        -identyfikatory nie mogą zawierać zmiennych

        Czyli dozwolone są takie konstrukcje:
            obj.prop_1, obj.prop$
        Ale takie już nie:
            obj.1prop, obj.prop name

        Notacja nawiasowa:
        -identyfikator pola musi być ciągiem znakowym albo zmienną wskazującą na taki ciąg
        -można używać zmiennych, spacji i ciągów znakowych zaczynających się od cyfr jako identyfikatorów

        Czyli dozwolone są takie konstrukcje:
            obj["1prop"], obj["prop name"]
*/

/* 

Użycie argumentów innego typu niż string dla operatora [] dostępu do składowej
obiektu.
– Co się dzieje jeśli argumentem operatora jest liczba?

*/
console.log("-----------------------");

console.log("EX1");
console.log(ex1[1]);
console.log(ex1['1']);
console.log(Object.keys(ex1));
console.log("EX2");
console.log(ex2[1]);
console.log(ex2['1']);
console.log(Object.keys(ex2));

/*

    Jak widzimy, mimo tego, że zdefiniowałem dwa pola: '1' oraz 1, z obiektu zwracana jest ta później zdefiniowana wartość.
    Przy tworzeniu obiektu zarówno pole 1 jak i '1' to dla języka to samo. Poźniej zdefiniowane pole przykrywa nam to wcześniejsze.
    Dlatego nie ważne jak zapytamy o pole, tutaj zwróci nam później zdefiniowaną wartość 
    z pola 1: bar. Widzimy po kluczach, że cyfry są konwertowane na string.

*/

/* 

– Co się dzieje jeśli argumentem operatora jest inny obiekt?

*/

console.log("-----------------------");
console.log(ex1[ex2]);
ex1[ex2] = 'someValue';
console.log(ex1);

/*

    Jak widać, po prostu następuje konwersja obiektu do stringa.

*/
/*

– Jaki wpływ na klucz pod jakim zapisana zostanie wartość ma programista w obu
przypadkach?

*/

/*

    Możemy na przykład dla obiektu zdefiniować valueOf. Wtedy przy próbie konwersji z obiektu zostanie zwróćona odpowiednia wartość.

*/

var ex3 = {
    1:      'someString',
    2:      'exampleString',

    toString: function() {
        return 'EX3';
    }

};

var ex4 = {
    1:      'otherString',
    2:      'otherExampleString',

    toString: function() {
        return 'EX4';
    }

};

ex3[ex4] = 'someVal';
console.log(Object.keys(ex3));
console.log(" ");

/*

    Jak widzimy w powyższym przykładzie, mogę na przykład dla obiektu dodać metodę "toString", 
    dzięki czemu mam kontrolę nad tym jaki klucz dodaję do obiektu.

    W przypadku, gdy argumentem jest liczba mogę po prostu napisać dowolną liczbę w postaci stringa lub zwykłęj liczby
    (nie ma różnicy, JavaScript i tak uzna to za string).

*/

/*

użycie argumentów innego typu niż number dla operatora [] dostępu do tablicy.
– Co się dzieje jeśli argumentem operatora jest napis?

*/

var tab = [1,2,3,4,5,"abc", 1.5];

console.log(tab["abc"]);
console.log(tab["length"]);

/*

    Jak widzimy, jeśli odwołamy się do nieistniejącego elementu w tablicy (abc), wtedy dostaniemy undefined.
    Dzieje się tak, ponieważ tablica nie ma takiego indeksu ani pola jak "abc".
    Inaczej sprawa się ma, gdy w indeksie wpiszemy np. "length", które jest poprawnym polem dla obiekty typu Array.
    Wtedy używamy po prostu notacji nawiasowej i zwracamy po prostu wartość pola "length", tutaj będzie to 7.

*/

/*

    Co się dzieje jeśli argumentem operatora jest inny obiekt?
 
 */

tab[ex1] = "someValueAdded";
console.log(tab[ex1]);
console.log(tab['[object Object]']);

 /*

    Jak widzimy, tablica zachowuje się tak samo jak obiekt. Tak więc jak indeksujemy innym obiektem,
    dostajemy pole postaci '[object Object]'.

*/

/*

    Czy i jak zmienia się zawartość tablicy jeśli zostanie do niej dopisana właściwość
    pod kluczem, który nie jest liczbą?

*/

/* 
    Jak widzieliśmy w poprzednim przykładzie, wartości kryjące się pod kluczami indeksowanymi liczbami nie zmieniają się.
*/

/* 

    Czy można ustawiać wartość atrybutu length tablicy na inną wartość niż liczba
    elementów w tej tablicy? Co się dzieje jeśli ustawia się wartość mniejszą niż liczba
    elementów, a co jeśli ustawia się wartość większą niż liczba elementów?

*/

console.log(tab);
tab.length = 0;
console.log(tab);

/* 

    Jak widzimy, zmiana długości tablicy w ten sposób usuwa nam po prostu wszystkie wartości
    kryjące się pod indeksami liczbowymi. Inne pola (tutaj pole [object Object]) pozostaje bez zmian.

*/
