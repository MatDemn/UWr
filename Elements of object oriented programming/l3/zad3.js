function forEach(a, f) {
    for(i of a) {
        f(i);
    }
}
function map(a, f) {
    var acc = [];
    for(i of a) {
        acc.push(f(i));
    }
    return acc;
}
function filter(a, f) {
    var acc = [];
    for(i of a) {
        if(f(i)) {
            acc.push(i);
        }
    }
    return acc;
}
var a = [1, 2, 3, 4];


forEach(a, _ => { console.log(_); });
// [1,2,3,4]
console.log(map(a, _ => _ * 2));
// [2,4,6,8]
console.log(filter(a, _ => _ < 3));
// [1,2]

