/*
    Definiuję obiekt, nadaję mu opisane w poleceniu pole, po jednym getterze i setterze oraz metodę.
*/

var osoba = {
    _name: "FooBar",

    introduceYourself: function() {
        console.log(`Hi! I'm ${osoba._name}. Nice to meet you!`);
    },

    get name() {
        return osoba._name;
    },

    set name(val) {
        osoba._name = val;
    }

};

console.log(osoba);

console.log(osoba._name);
console.log(osoba.name);
console.log(osoba.name = "BarFoo");
osoba.introduceYourself();

/* 
    Jak widzimy wszystko działa prawidłowo. Teraz spróbuję dodać te pola do pustej osoby 
    za pomocą . oraz [] i sprawdzę czy gdzieś powstanie błąd.
*/

var osoba2 = {};

osoba2._name = "FooBar";
osoba2.get = function() {
    return osoba2._name;
};
osoba2.set = function(val) {
    osoba2._name = val;
};

osoba2.introduceYourself = function() {
    console.log(`Hi! I'm ${osoba._name}. Nice to meet you!`);
}

console.log(osoba2);

console.log(osoba2._name);
console.log(osoba2.name = "BarFoo");
console.log(osoba2.name);
osoba.introduceYourself();
console.log(osoba2);

/*
    Jak widzimy, nie udało się w ten sposób dodać settera oraz gettera (zgodnie z tym co jest w notatkach).
    Za pomocą defineProperty możemy dodać wszystko.
*/

osoba2 = {};

Object.defineProperty (osoba2, '_name', {
    value: "BarFoo"
});

Object.defineProperty (osoba2, 'introduceYourself', {
    value: function() {
        console.log(`Hi! I'm ${osoba._name}. Nice to meet you!`);
    }
});

Object.defineProperty (osoba2, 'name', {
    get: function() {
        return osoba._name;
    },

    set: function(val) {
        osoba._name = val;
    }
});

console.log("Obiekt stworzony od razu i poniżej, z dołączonymi polami przez defineProperty");
console.log(osoba)
console.log(osoba2);

/*
    Jak widzimy, obiekty są identyczne.
*/

