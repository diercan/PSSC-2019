'use strict';

const winston = require('winston');
require('winston-daily-rotate-file');


const LogService = (function() {
  let instance = null;

  function LogService() {
    const fileTransport = new(winston.transports.DailyRotateFile)({

      levels: winston.config.syslog.levels,
      level:  'debug',
      filename: './logs/TrainingScheduler-%DATE%.log',
      datePattern: 'YYYY-MM-DD-HH',
      zippedArchive: false,
      maxSize: 10240000,
      maxFiles: 100,
      timestamp: () => {
        const today = new Date();
        return '[' + process.pid + '] ' + today.toISOString();
      },
      silent: false

    });

    const t = {
      console: new winston.transports.Console({
        levels: winston.config.syslog.levels,
        level: 'debug',
      }),
      file: fileTransport,
    };

    const logger =  new (winston.Logger)({
      transports: [
        t.console,
        t.file
      ]
    });

    this.logger = logger;
  }

  return {

    getInstance: function() {
      if (instance === null) {
        instance = new LogService();
        instance.constructor = null;
      }
      return instance.logger;
    },
  };
})();

module.exports = LogService.getInstance();
