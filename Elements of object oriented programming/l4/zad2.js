function Foo() {
    console.log("Foo");
}

Foo.prototype.Bar = function() {
    function Qux() {
        console.log("I'm Qux. Yay!");
    }
    Qux();
    console.log("And I'm Bar. Nice to meet you, user. :)");
}

var f = new Foo();

f.Bar();        // Tu wszystko gra
//f.Qux();      // Wygeneruje błąd "not a function"
//f.Bar.Qux();  // Też wygeneruje błąd "not a function"