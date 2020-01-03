const {
    Requestdb,
    Usersdb
  } = require('../models')
  const Sequelize = require('sequelize')
    
  const sickleave = async (req, res) => {
      console.log("me");
      console.log(req.body);
      const { 
          name,
          personnelID,
          username,
          startdate,
          enddate,
          days,
          code
       
      } = req.body;
      var flag = false;
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
          
           console.log("USER", user);
           if(user !== null){
               flag=true;
           } 
           
            if(flag){
                try {
   
    
                    const newUser = Requestdb.create({
                        requestdb_name: name,   
                        requestdb_personnelID:personnelID,
                        requestdb_username:username,
                        requestdb_startdate:startdate,
                        requestdb_enddate:enddate,
                        requestdb_days:days,
                        requestdb_code:code
                       
                    });
                    res.status(200).send("request send");
                    
                  } catch (err) {
                    res.status(400).json({
                      error: err
                    });
                  }
                
            } else{
                 res.status(400).send("failed");
            }
         });    
   
}
const getDays = async(req,res)=>{
    const{requestdb_personnelID}=req.body;
    var totalDays=0;
    console.log(requestdb_personnelID);
    Requestdb.findAll({
            attributes:['requestdb_personnelID', 'requestdb_days']
    }).then( event=>{
        event.forEach(element => {
            console.log(element.requestdb_personnelID);
            if(element.requestdb_personnelID==requestdb_personnelID)
            {
            
                totalDays=element.requestdb_days+totalDays;

            }
            console.log("pana aici");
           
        });
        console.log(totalDays);
        res.send(totalDays.toString());
    })
}

  module.exports = {
    '/request2': {

      post: {
        action: sickleave,
        level: 'public'
      }
    },
      '/requestdays': {

        post: {
          action: getDays,
          level: 'public'
        }
      },
  
  }