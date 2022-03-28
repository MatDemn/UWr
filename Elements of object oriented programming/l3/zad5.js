function sum(...tab) {
    var acc = 0;

    for(i of tab) {
        acc += i;
    }
    return acc;
}
console.log(sum(1, 2, 3));
// 6
console.log(sum(1, 2, 3, 4, 5));
// 15
