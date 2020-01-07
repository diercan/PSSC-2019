var q = 'externalLogs';
var url = "amqp://fsdyhdee:X42mSkbee6ZcKgNihz115rjx1JeXd16G@stingray.rmq.cloudamqp.com/fsdyhdee";
var amqplib = require('amqplib').connect(url);

amqplib.then(function (conn) {
  console.log("connected")
  var ok = conn.createChannel();
  ok = ok.then(function (ch) {
    ch.assertQueue(q);
    ch.consume(q, function (msg) {
      if (msg !== null) {
        console.log(msg.content.toString());
        ch.ack(msg);
      }
    });
  });
  return ok;
}).then(null, console.warn);