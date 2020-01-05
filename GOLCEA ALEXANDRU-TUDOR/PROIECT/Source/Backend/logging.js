var textColor = require(__dirname + "/textColor.js")
var magicglobals = require('magic-globals')

function _getCallerFile() {
    try {
        var err = new Error();
        var callerfile;
        var currentfile;

        Error.prepareStackTrace = function (err, stack) { return stack; };

        currentfile = err.stack.shift().getFileName();

        while (err.stack.length) {
            callerfile = err.stack.shift().getFileName();

            if(currentfile !== callerfile) return callerfile;
        }
    } catch (err) {}
    return undefined;
}

function testLogging() {
    LOG("TEST LOG")
    ERR("TEST ERR")
    DBG("TEST DBG")
}



function LOG(msg) {
    let e = new Error();
    let stack = e.stack.toString();
    var stacks = stack.split("\n");
    var stacksString = stacks.toString()
    let lineNumber = stacksString.split(":")[5];
    let fileName = stacksString.split(":")[4];
    console.log("\x1b[32m"+"LOG:"+"\x1b[37m"+ " File "+"\x1b[32m"+fileName+"\x1b[37m"+" | Line "+"\x1b[32m"+lineNumber+"\x1b[37m"+" | "+"\x1b[32m"+msg+"\x1b[37m")
}

function ERR(msg) {
    let e = new Error();
    let stack = e.stack.toString();
    var stacks = stack.split("\n");
    var stacksString = stacks.toString()
    let lineNumber = stacksString.split(":")[5];
    let fileName = stacksString.split(":")[4];
    console.error("\x1b[31m"+"ERR:"+"\x1b[37m"+ " File "+"\x1b[31m"+fileName+"\x1b[37m"+" | Line "+"\x1b[31m"+lineNumber+"\x1b[37m"+" | "+"\x1b[31m"+msg+"\x1b[37m")
}

function DBG(msg) {
    let e = new Error();
    let stack = e.stack.toString();
    var stacks = stack.split("\n");
    var stacksString = stacks.toString()
    let lineNumber = stacksString.split(":")[5];
    let fileName = stacksString.split(":")[4];

    console.debug("\x1b[33m"+"DBG:"+"\x1b[37m"+ " File "+"\x1b[33m"+fileName+"\x1b[37m"+" | Line "+"\x1b[33m"+lineNumber+"\x1b[37m"+" | "+"\x1b[33m"+msg+"\x1b[37m")
}

function GOD(msg)
{
    console.log("\x1b[34m"+" "+msg+"\x1b[37m");
}

module.exports =
    {
        LOG: LOG,
        ERR: ERR,
        DBG: DBG,
        GOD: GOD,
        testLogging: testLogging,
        textColor: textColor,
        magicglobals: magicglobals
    }