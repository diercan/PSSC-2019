const {
    Usersdb
  } = require('../models')
  const Sequelize = require('sequelize')

var passwordHash = require('password-hash');

  const login = async (req, res) => {
    const {username, password, personnelID} = req.body;

    Usersdb.findOne({ 
      where: {
        usersdb_personnelID: personnelID
      },
      attributes: [
        "usersdb_name",
        "usersdb_mail",
        "usersdb_username",
        "usersdb_password",
        "usersdb_personnelID",
        "usersdb_company"
      ]

    }).then(user => {
       let flag = false;
      console.log("USER", user);
      if(user !== null){

        if(passwordHash.verify(password,user.usersdb_password))
        {
            flag=true;
        }
      } 
      
       if(flag){
            res.status(200).send({user});
       } else{
            res.status(400).send("failed login");
       }
    });
  }
  
  

  
  const reg = async (req, res) => {
    try {
        console.log(req.body);
        const { 
            name,
            mail,
            username,
            password,
            personnelID,
            company
        } = req.body;
        var hashPass = passwordHash.generate(password);
        const newUser = await Usersdb.create({
            usersdb_name: name,
            usersdb_mail:mail,
            usersdb_username:username,
            usersdb_password:hashPass,
            usersdb_personnelID:personnelID,
            usersdb_company:company
           
        });
        res.status(200).send("register successful");
        
      } catch (err) {
        res.status(400).json({
          error: err
        });
      }
       
    
  }
  
const details = async (req, res) => {
    const {personnelID} = req.body;
    Usersdb.findOne({ 
        where: {
          usersdb_personnelID: personnelID
        },
        attributes: [
          "usersdb_name",
          "usersdb_mail",
          "usersdb_username",
          "usersdb_personnelID",
          "usersdb_company"
        ]
  
      }).then(user => {
          data=user;
        res.status(200).send(data);
    });
}
  
  module.exports = {
    '/login2': {

      post: {
        action: login,
        level: 'public'
      }
    },
    '/register2': {
      post: {
        action: reg,
        level: 'public'
      }
    },
    '/details2':{
        post: {
            action: details,
            level: 'public'
          }
    }
  }