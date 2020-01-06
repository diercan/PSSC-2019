'use strict';

const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');
const HttpError = require('../util/httpError');
const TrainingController = require('./trainingController');
const UserController = require('./userController');
var User = require('../models/userModel');
const fs = require('fs');
const UserRepository = require('../models/repositories/userRepository');
module.exports = function (log, config, db) {
  let userRepository = new UserRepository(config, db)
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
    
    let user = new User(null, userName, null, null, null, pass)
    const authUser = await userRepository.getUsersWithPassword(user.password, user.userName);
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
  const trainingRepository = new (require('../models/repositories/trainingRepository'))(config, db);
  app.use(config.endpoints.trainingContext, new TrainingController(trainingRepository));
  app.use(config.endpoints.userContext, new UserController(userRepository));

  // middlewares
  // =============================================================================
  app.use(require('./../middlewares/errorJsonResponse')());


  return app;
};
