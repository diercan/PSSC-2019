var q = 'externalLogs';
var logging = require(__dirname + "/logging.js")
var url = process.env.CLOUDAMQP_URL || "amqp://lbvbrkmn:QFrbXeUqW2ut1wv96QeFUUnoy6jZxHDG@stingray.rmq.cloudamqp.com/lbvbrkmn";
var open = require('amqplib').connect(url);

open.then(function(conn) {
    console.log("connected")
    var ok = conn.createChannel();
    ok = ok.then(function(ch) {
      ch.assertQueue(q);
      ch.consume(q, function(msg) {
        if (msg !== null) {
        //   console.log(msg.content.toString());
          if(msg.content.toString().includes("SUCCESS")){
            logging.GOD(msg.content.toString());
          } else {
            logging.ERR(msg.content.toString());

          }
          ch.ack(msg);
        }
      });
    });
    return ok;
  }).then(null, console.warn);