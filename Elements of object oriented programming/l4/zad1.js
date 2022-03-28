function Tree(val, left, right) {
    this.left = left;
    this.right = right;
    this.val = val;
}
    
/*
// Działanie na kolejce. Każdy poziom drzewa jest wypisywany "od lewej do prawej"
Tree.prototype[Symbol.iterator] = function*() {
    var stack = [this];
    while(stack.length) {
        //var elem = stack.pop();
        var elem = stack.splice(0, 1);
        if(elem[0].left) {
            stack.splice(stack.length, 0, elem[0].left);
        }

        if(elem[0].right) {
            stack.splice(stack.length, 0, elem[0].right);
        }
        yield elem[0].val;
    }
}*/
// Wynik tutaj będzie:
// 1 2 4 3


// Alternatywa: działanie na stosie. Każdy poziom drzewa jest wypisywany "od prawej do lewej" - przeciwnie niż poprzednio
Tree.prototype[Symbol.iterator] = function*() {
    var stack = [this];
    while(stack.length) {
        //var elem = stack.pop();
        var elem = stack.splice(stack.length-1, 1);
        if(elem[0].left) {
            stack.splice(stack.length, 0, elem[0].left);
        }

        if(elem[0].right) {
            stack.splice(stack.length, 0, elem[0].right);
        }
        yield elem[0].val;
    }
}

// Wynik tutaj to będzie:
// 1 4 2 3
    
var root = new Tree( 1, 
                    new Tree( 2, 
                        new Tree( 3 ) ), 
                    new Tree( 4 ));
    
for ( var e of root ) {
    console.log( e );
}

// Konstrukcja drzewa:
//      1
//      /\
//     2  4
//    /
//   3

// Polecenie mówi o wykorzystaniu kolejki.
// Elementy będą wkładane w kolejności:
// 1, 2, 4, 3
// W takiej też kolejności będą z niej wyjmowane (wynika z natury kolejki).
// Jednak jeśli użyjemy stosu, wynik będzie następujący:
// 1, 4, 2, 3
    