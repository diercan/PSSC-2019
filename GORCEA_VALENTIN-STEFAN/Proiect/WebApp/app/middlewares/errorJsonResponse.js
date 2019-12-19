'use strict';

module.exports = function() {
  return function(err, req, res, next) {
    if (res.headersSent) {
      return next(err);
    }
    const statusCode = err.statusCode || 500;
    const errorResponse = {
      statusCode: statusCode,
      message: err.message,
    };
    res.status(statusCode).json(errorResponse);
  };
};

