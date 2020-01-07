function Publish(message) {
    var q = 'externalLogs';
    // var logging = require(__dirname + "logging.js")
    var url = process.env.CLOUDAMQP_URL || "amqp://lbvbrkmn:QFrbXeUqW2ut1wv96QeFUUnoy6jZxHDG@stingray.rmq.cloudamqp.com/lbvbrkmn";
    var open = require('amqplib').connect(url);
    // logging.GOD(`Logging publisher service started.`)


    console.log("Intra aici");

    open.then(function (conn) {
        console.log("connected");
        var ok = conn.createChannel();
        ok = ok.then(function (ch) {
            ch.assertQueue(q);
            ch.sendToQueue(q, new Buffer(message));
        });
        return ok;
    }).then(null, console.warn);







};

module.exports =
    {
        Publish: Publish,
    }
