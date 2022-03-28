module.exports.b = b;
var a = require("./a.js");

function b() {
    console.log("I'm in b file");
}

a.a();