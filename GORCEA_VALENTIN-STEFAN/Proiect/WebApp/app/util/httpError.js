'use strict';
const log = require('../middlewares/logService');

class HttpError extends Error {
  constructor(statusCode, message, fileName, lineNumber) {
    super(message, fileName, lineNumber);
    log.error(message);
    this.statusCode = parseInt(statusCode, 10);
    this.isClientError = String(this.statusCode).charAt(0) === '4';
    this.isServerError = !this.isClientError;
  }
}

module.exports = HttpError;
