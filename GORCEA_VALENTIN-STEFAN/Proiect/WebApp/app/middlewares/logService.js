'use strict';
const publishToQueue = require('./RBTMQService');

const LogService = (function() {
  let instance = null;
  function Log(){
    this.debug = function(message){
    publishToQueue.publish("debug", message);
    },
    this.error = async function(message){
    publishToQueue.publish("errors", message);
    }
  }

  return {
    getInstance: function() {
      if (instance === null) {
        instance = new Log();
      }
      return instance;
    },
  };
})();

module.exports = LogService.getInstance();
