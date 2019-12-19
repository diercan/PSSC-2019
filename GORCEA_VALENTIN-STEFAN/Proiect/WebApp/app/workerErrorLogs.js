var amqp = require('amqplib/callback_api');
const CONN_URL = 'amqp://mfcfrnsg:NMx4VZXKhJ4EKKmAaJIc5Kak0zO8JSDF@dove.rmq.cloudamqp.com/mfcfrnsg';
amqp.connect(CONN_URL, function (err, conn) {
 
  conn.createChannel(function (err, ch) {
    ch.consume('errors', function (msg) {
      setTimeout(function(){
        console.log("Error:", msg.content.toString());
      },4000);
      },{ noAck: true }
    );
  });
});