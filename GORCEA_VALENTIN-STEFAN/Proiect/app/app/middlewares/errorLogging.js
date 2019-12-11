'use strict';

const HttpError = require('./../util/httpError');

module.exports = function() {
  return function(err, req, res, next) {
    if (err instanceof HttpError && err.isClientError === true) {
      console.log(`Error code ${err.statusCode}, message ${err.message}`);
    } else {
      console.log(`Error code ${err.statusCode}, message ${err.message} stack ${err.stack}`);
    }
    next(err);
  };
};
