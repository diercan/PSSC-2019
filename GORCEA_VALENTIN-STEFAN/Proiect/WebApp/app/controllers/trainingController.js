'use strict';

const express = require('express');
const HttpError = require('../util/httpError');
const log = require('../services/logService');

const trainingRouter = express.Router();

const router = function (trainingService) {
    
   
    // get all trainings
    trainingRouter.get('/', async (req, res, next) => {
        try {
            const trainings = await trainingService.getTrainings(req.authenticatedUser.userName);

            const result = { 
                user : req.authenticatedUser.userName,
                trainings : trainings
            }; 

            res.setHeader('Status', 200);
            log.debug(result);
            res.send(result);
        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `GET / training error ${err}.`));
        }
    });

    // create a training
    trainingRouter.post('/new', async (req, res, next) => {
        try {
            const result = await trainingService.createTraining(req.body, req.authenticatedUser);
    
            res.setHeader('Status', 200);
            res.send(result);
    
        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `POST join-training error ${err}.`));
        }
    });

    // join a training
    trainingRouter.post('/:trainingId', async (req, res, next) => {
        try {
            const result = await trainingService.joinTraining(req.params.trainingId, req.authenticatedUser.userName, true);
    
            res.setHeader('Status', 200);
            res.send(result);
    
        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `POST join-training error ${err}.`));
        }
    });

     // give up on a training
     trainingRouter.put('/leave/:trainingId', async (req, res, next) => {
        try {
             const result = await trainingService.cancelJoinTraining(req.params.trainingId, req.authenticatedUser.userName);

            res.setHeader('Status', 200);
            res.send(result);
        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `DELETE join-training error ${err}.`));
        }
    });

    // cancel a training
    trainingRouter.delete('/:trainingId', async (req, res, next) => {
        try {
             const result = await trainingService.deleteTraining(req.params.trainingId, req.authenticatedUser.userName);
            if(result === -1){
                res.setHeader('Status', 200);
                res.send([]);
            }else{
                res.setHeader('Status', 200);
                res.send(result);
            }
          
        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `DELETE join-training error ${err}.`));
        }
    });

   

    return trainingRouter;
};

module.exports = router;