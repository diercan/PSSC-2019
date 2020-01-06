const amqp = require('amqplib/callback_api');

const CONN_URL = 'amqp://mfcfrnsg:NMx4VZXKhJ4EKKmAaJIc5Kak0zO8JSDF@dove.rmq.cloudamqp.com/mfcfrnsg';
//let ch = null;

const amqpModule = (function (){
    let instance = null;
    let ch = null
    function amqpService(){
        this.connect = function(){
            amqp.connect(CONN_URL, function (err, conn) {
                conn.createChannel(function (err, channel) {
                   ch = channel;
                });
             });
        },
        this.publish = async function(queueName, data){
            if (ch === null){
                amqp.connect(CONN_URL, function (err, conn) {
                    conn.createChannel(function (err, channel) {
                       ch = channel;
                       ch.sendToQueue(queueName, new Buffer(data));
                    });
                 });
            }
            else{
            ch.sendToQueue(queueName, new Buffer(data));
          }
        },
        this.close = function(){
            ch.close();
        }
      }
      return {
        getInstance: function() {
          if (instance === null) {
            instance = new amqpService();
          }
          return instance;
        },
      };

})();

module.exports = amqpModule.getInstance()