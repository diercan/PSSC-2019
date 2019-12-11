'use strict';

const express = require('express');
const HttpError = require('../util/httpError');
const log = require('../services/logService');

const userRouter = express.Router();

const router = function (userService) {

    // get users details
    userRouter.get('/', async (req, res, next) => {
        try {
            const user = await userService.getUsers();

            res.setHeader('Status', 200);
            res.send(user);

        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `GET me error ${err}.`));
        }
    });

    userRouter.get('/me', async (req, res, next) => {
        try {
            const user = await userService.getUser(req.authenticatedUser.userName);

            res.setHeader('Status', 200);
            res.send(user);

        } catch (err) {
            log.error(JSON.stringify(err));
            return next(new HttpError(500, `GET me error ${err}.`));
        }
    });

    return userRouter;
};

module.exports = router;