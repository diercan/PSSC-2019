
const {
  User
} = require('../models')
const Sequelize = require('sequelize')
const bcrypt = require("bcryptjs");
const logService = require("../loggingService.js");


const login = async (req, res) => {
  const { email, password } = req.body;

  const user = await User.findOne({
    where: {
      email: email
    },
    attributes: [
      "id",
      "firstName",
      "lastName",
      "email",
      "password"
    ]

  })
  let flag = false;
  
  if (user !== null) {
    flag = await bcrypt.compare(password, user.password);
  }

  if (flag) {
    logService.Publish("LOG-SUCCESS:: Loging user successfuly.");
    res.status(200).send({ id: user.id });
  } else {
    logService.Publish("LOG-ERROR:: Failed login.");
    res.status(400).send("failed login");
  }
}

const findUser = async (req, res) => {
  User.findOne({
    where: {
      id: req.params.id
    },
    attributes: [
      "fistName",
    ]
  }).then(user => {
    logService.Publish("LOG-SUCCESS:: Find user.");
    res.status(200).send(user);
  });

}



const reg = async (req, res) => {
  try {
    const {
      firstName,
      lastName,
      email,
      password,
    } = req.body;
    const salt = await bcrypt.genSalt(10);
    const cyptPass = await bcrypt.hash(password, salt);
    console.log(cyptPass)
    const newUser = await User.create({
      firstName,
      lastName,
      email,
      "password": cyptPass,
    });
    logService.Publish("LOG-SUCCESS:: Success registring user.");
    res.status(200).send({id : newUser.id});

  } catch (err) {
    logService.Publish("LOG-ERROR:: Error registring user.");
    res.status(400).json({
      error: err
    });
  }
}



module.exports = {
  '/login': {

    post: {
      action: login,
      level: 'public'
    }
  },
  '/register': {
    post: {
      action: reg,
      level: 'public'
    },
  },
  '/profile/:id': {
    get: {
      action: findUser,
      level: 'public'
    }
  }
}
