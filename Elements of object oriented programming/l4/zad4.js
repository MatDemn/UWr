/*
const { stdout } = require("process");
const readline = require("readline");

const rl = readline.createInterface({
    input: process.stdin, 
    output: stdout
});

rl.question("What's your name, user?", (answer) => {
    console.log(`Your name is ${answer}. Nice to meet you!`);

    rl.close();
});
*/
process.stdout.write("What's your name, user?");
process.stdout.write("\n");
process.stdin.on('readable', function() {
    var name = process.stdin.read();
    process.stdout.write(`Your name is ${name}`);
});


