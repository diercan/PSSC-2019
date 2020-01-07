let logging = require("../logging.js")
const { Event, User } = require('../models')
const Sequelize = require('sequelize')
// const bcrypt = require("bcrypt-nodejs");
const logService = require("../loggingService.js");


const insertEvent = async (req, res) => {
    try {
        const id = req.params.id;
        const {
            title,
            description,
            startDate,
            endDate
        } = req.body;
        const newEvent = await Event.create({
            title,
            description,
            startDate,
            endDate,
            "UserId": id
        });
        logService.Publish("LOG-SUCCESS:: Inserted new event.");
        res.status(200).send("inserted date");
    } catch (err) {
        logService.Publish("LOG-ERROR:: Error inserting event.");
        res.status(400).json({
            error: err
        });
    }
}

const getAllEvents = async (req, res) => {
    const id = req.params.id;


    User.findOne({
        where: {
            id: id
        },
        attributes: [
            "id",
            "email"
        ],
        include: [
            {
                model: Event,
                as: 'events',
                attributes: ["title", "description", "startDate", "endDate"]
            }
        ]

    }).then(event => {
        logging.LOG("Event " + event)
        if (event !== null) {
            logService.Publish("LOG-SUCCESS:: FIND EVENTS. " + event );
            res.status(200).send(event);
        } else {
            logService.Publish("LOG-ERROR:: ERROR FINDING EVENTS.");
            res.status(400).send("Event Doesn't Exist");
        }

    });


}



module.exports = {
    '/insertEvent/:id': {
        post: {
            action: insertEvent,
            level: 'public'
        }
    },
    '/getAllEvents/:id': {
        get: {
            action: getAllEvents,
            level: 'public'
        }
    },
  
}
