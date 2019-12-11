'use strict';

const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const HttpError = require('../util/httpError');
const TrainerController = require('./trainingController');
const UserController = require('./userController');
const basicAuth = require('express-basic-auth')

module.exports = function (log, config, dbService) {
  const app = express();

/* 
  app.use(basicAuth({
    users: { 'Foo': 'bar' },
    unauthorizedResponse: getUnauthorizedResponse
}))
 
function getUnauthorizedResponse(req) {
  console.log(req.auth, req.path)
    return req.auth
        ? ('Credentials ' + req.auth.user + ':' + req.auth.password + ' rejected')
        : 'No credentials provided'
} */

  // cors
  // =============================================================================
  const corsOptions = {
    origin: config.webServer.corsAllowed,
    optionsSuccessStatus: 200,
    credentials: true,
  };

  app.use(cors(corsOptions));

  app.use(bodyParser.json());
  app.use(bodyParser.urlencoded({
    extended: false,
  }));

  // register context
  // =============================================================================
  const trainingService = new (require('./../services/trainingService'))(config, dbService);
  const userService = new (require('./../services/userService'))(config, dbService);
  app.use(config.endpoints.trainingContext, new TrainerController(trainingService));
  app.use(config.endpoints.userContext, new UserController(userService));

  // middlewares
  // =============================================================================
  app.use(require('./../middlewares/errorLogging')());
  app.use(require('./../middlewares/errorJsonResponse')());


  return app;
};
