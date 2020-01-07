'use strict';

const express = require('express');
const HttpError = require('../util/httpError');
const log = require('../middlewares/logService');
var User = require('../models/userModel');

const userRouter = express.Router();

const router = function (userRepository) {

    // get users details
    userRouter.get('/', async (req, res, next) => {
        try {
            const user = await userRepository.getUsers();

            res.setHeader('Status', 200);
            res.send(user);

        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `GET me error ${err}.`));
        }
    });

    userRouter.get('/me', async (req, res, next) => {
        try {
            let user = new User(null, req.authenticatedUser.userName);
            const result = await userRepository.getUser(user.userName);

            res.setHeader('Status', 200);
            res.send(result);

        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `GET me error ${err}.`));
        }
    });

    return userRouter;
};

module.exports = router;