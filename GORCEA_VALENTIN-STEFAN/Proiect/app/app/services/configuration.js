'use strict';

const fs = require('fs');

const ConfigurationService = (function () {
  let instance = null;

  function ConfigurationService() {
    this.config = JSON.parse(fs.readFileSync('config.json', 'utf8'));
  }

  return {

    getInstance: function () {
      if (instance === null) {
        instance = new ConfigurationService();
        instance.constructor = null;
      }
      return instance;
    },
  };
})();

module.exports = ConfigurationService.getInstance().config;
