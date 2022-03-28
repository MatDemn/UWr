var fs = require("fs");
var readline = require("readline");

const readInterf = readline.createInterface({
    input: fs.createReadStream("./httpserver.txt"),
    console: false
});

var dict = {};
readInterf.on('line', function(line) {
    var ipAddress = line.split(" ")[1];
    if(dict[ipAddress])
        dict[ipAddress]++;
    else
        dict[ipAddress] = 1;
}).on('close', function() {
    var sortedArr = [];

    for(elem in dict) {
        sortedArr.push([elem, dict[elem]]);
    }

    sortedArr.sort((x,y) => { return y[1] - x[1] });

    for(var i = 0; i<3; i++) {
        console.log(sortedArr[i][0]);
    }
});





