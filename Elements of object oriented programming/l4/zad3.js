var a = require("./a.js");
var b = require("./b.js");
// Będę tutaj używał dwóch pomocniczych plików (a.js oraz b.js),
// w celu demonstracji zagadnienia cyklu odwołań.
// Po uruchomieniu widzimy, że obie funkcje się wykonały.
// Dzieje się tak dlatego, że umieściliśmy linijkę "exports" 
// na samej górze i wykonuje się ona jako pierwsza.
// Gdyby ta linijka była niżej, wystąpiłby błąd (było o tym na wykładzie).

