module.exports.a = a;
var b = require("./b.js");

function a() {
    console.log("I'm in a file");
}

b.b();