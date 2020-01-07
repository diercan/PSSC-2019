function Logs(message) {
    var log = 'externalLogs';
    var url = "amqp://fsdyhdee:X42mSkbee6ZcKgNihz115rjx1JeXd16G@stingray.rmq.cloudamqp.com/fsdyhdee";
    var amqplib = require('amqplib').connect(url);

    amqplib.then(function (conn) {
        var channel = conn.createChannel();
        channel = channel.then(function (ch) {
            ch.assertQueue(log);
            ch.sendToQueue(log, new Buffer.from(message));
        });
        return channel;
    }).then(null, console.warn);

};

module.exports = {
    Logs: Logs,
}