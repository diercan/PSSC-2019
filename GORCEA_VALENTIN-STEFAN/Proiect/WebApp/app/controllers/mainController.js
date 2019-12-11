'use strict';

const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const HttpError = require('../util/httpError');
const TrainerController = require('./trainingController');
const UserController = require('./userController');

module.exports = function (log, config, dbService, userService) {
  const app = express();

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

  app.all('*', async(req,res,next)=>{
    try{
    let pass = JSON.parse(req.headers['authorization']).password;
    let userName = JSON.parse(req.headers['authorization']).userName;
    const authUser = await userService.getUsersWithPassword(pass,userName);
    if(authUser === -1){
      return next(new HttpError(401, `Unauthorized`));
    }
    req.authenticatedUser = authUser[0];
    next();
    }
    catch(err){
      log.error(err)
      return next(new HttpError(401, `Unauthorized`));
    }
  })

  // register context
  // =============================================================================
  const trainingService = new (require('./../services/trainingService'))(config, dbService);
  //const userService = new (require('./../services/userService'))(config, dbService);
  app.use(config.endpoints.trainingContext, new TrainerController(trainingService));
  app.use(config.endpoints.userContext, new UserController(userService));

  // middlewares
  // =============================================================================
  app.use(require('./../middlewares/errorLogging')());
  app.use(require('./../middlewares/errorJsonResponse')());


  return app;
};
