var express = require('express');
var path = require('path');
var serveStatic = require('serve-static');
var nodemailer = require('nodemailer');
var passwordHash = require('password-hash');
app = express();
app.use(express.json());

app.use(serveStatic(__dirname + "/dist"));
var port = process.env.PORT || 5000;
app.listen(port);
console.log('server started ' + port);
var connection;
var mysql = require('mysql')

// Register WEB Begin
app.post('/api/register', function (req, response) {
  response.send("Register");
  insertpeople(req.body.name, req.body.mail, req.body.username, req.body.password, req.body.persId);
});

function insertpeople(name, mail, username, password, persId) {
  var hashPass = passwordHash.generate(password);
  console.log(hashPass);
  var save_result
  connection.query("INSERT INTO users(name,mail,username,password,persId) VALUES('" + name + "','" + mail + "','" + username + "','" + hashPass + "','" + persId + "')", function (err, result2) {
    if (err) throw err
    save_result = result2;
    console.log('The solution is1: ', result2)
  })

}
// Register WEB End 

// Insert Crash alert WEB Begin
function insertcrash(car_nr, car_owner, car_crashSensor, car_GPS, car_temperatureSensor, car_fireSensor, car_EmergencyPerson, car_date) {

  var save_result_car
  connection.query("INSERT INTO car_data(car_nr, car_owner, car_crashSensor, car_GPS, car_temperatureSensor,car_fireSensor,car_EmergencyPerson,car_date) VALUES('" + car_nr + "','" + car_owner + "','" + car_crashSensor + "','" + car_GPS + "','" + car_temperatureSensor + "','" + car_fireSensor + "','" + car_EmergencyPerson + "','" + car_date + "')", function (err, resultcar) {
    if (err) throw err
    save_result_car = resultcar;
    console.log('The solution is: ', resultcar)
  })
  connection.query("INSERT INTO car_data_backup(car_nr, car_owner, car_crashSensor, car_GPS, car_temperatureSensor,car_fireSensor,car_EmergencyPerson,car_date) VALUES('" + car_nr + "','" + car_owner + "','" + car_crashSensor + "','" + car_GPS + "','" + car_temperatureSensor + "','" + car_fireSensor + "','" + car_EmergencyPerson + "','" + car_date + "')", function (err, resultcar) {
    if (err) throw err
    save_result_car = resultcar;
    console.log('The solution is: ', resultcar)
  })

}

app.post('/api/addCrashEvent', function (req, response) {
  response.send("Added successfully!");
  insertcrash(req.body.car_nr, req.body.car_owner, req.body.car_crashSensor, req.body.car_GPS, req.body.car_temperatureSensor, req.body.car_fireSensor, req.body.car_EmergencyPerson, req.body.car_date);
});
// Insert Crash alert WEB End

// Delete Crash alert WEB Begin
function deleteCrash(idcar) {
  var save_result_car
  connection.query("DELETE FROM car_data WHERE idcar=" + idcar, function (err, resultcar) {
    if (err) throw err
    save_result_car = resultcar;
    console.log('The solution is: ', resultcar)
  })
}
app.post('/api/deleteCarEvents', function (req, response) {
  response.send("Event sters");
  deleteCrash(req.body.idcar);
});
// Delete Crash alert WEB End

// Delete User alert WEB Begin
function deleteUser(persId) {
  var save_result_user
  connection.query("DELETE FROM users WHERE persId=" + persId, function (err, resultUser) {
    if (err) throw err
    save_result_user = resultUser;
    console.log('The solution is: ', resultUser)
  })
}
app.post('/api/deleteUser', function (req, response) {
  response.send("User sters");
  deleteUser(req.body.persId);
});
// Delete User alert WEB End

// Login WEB Begin
app.post('/api/login', function (req, response) {
  var save_result
  connection.query("SELECT * from users WHERE persId='" + req.body.persId + "'", function (err, rows, result2) {
    if (err) throw err
    save_result = result2;

    console.log(rows.length);
    if (rows.length == 1) { //verify personel ID
      var hashedPassword = passwordHash.generate(req.body.password);
      console.log(hashedPassword);
      console.log(rows[0].password);
      if (rows[0].username == req.body.username && (passwordHash.verify(req.body.password, rows[0].password))) { //verify user and pass
        console.log(rows[0].username);
        console.log(rows[0].password);
        userData = {}; //object
        userData.username = rows[0].username; //save data in obj userData
        userData.password = rows[0].password;
        userData.persId = rows[0].persId;
        userData.name = rows[0].name;
        userData.mail = rows[0].mail;
        console.log("True");
        response.send(userData);
      }
      else {
        response.send("Username or password invalid!")
      }
    }
    else {
      console.log("False");
      response.send("Doesn't exist!");
    }
  })

});
app.get('/api/loginData', (request, result) => {

  result.status(200).send({
    data: "test"
  })
})
// Login WEB End

// Contact WEB Begin

function sendMail(name, mail, mesaj) {
  //create transport as SMTP with gmail service
  var transporter = nodemailer.createTransport({
    service: 'gmail',
    auth: {
      user: 'licentafast.2019@gmail.com',
      pass: 'Cristina1998'
    }
  });
  var mailOptions = {
    from: name,
    to: 'licentafast.2019@gmail.com',
    subject: mail,
    text: mesaj,
  };
  transporter.sendMail(mailOptions, function (error, info) {
    if (error) {
      console.log(error);
    }
    else {
      console.log('Email:' + info.response);
    }
  });
}
app.post('/api/mailcontacts', function (req, response) {
  response.send("Oki doki");
  sendMail(req.body.name, req.body.mail, req.body.mesaj);
});
// Contact WEB End

//Car Events WEB Begin
app.post('/api/getCrashEvents', function (req, response) {
  var save_resultcrash
  connection.query("SELECT * from car_data", function (err, rows, resultcarcrash) {
    if (err) throw err
    save_resultcrash = resultcarcrash;
    response.send(rows);
  })

});

//Car Events WEB End

// Mobile Register Begin 

app.post('/api/registermobile', (req, response) => {
  var hashPass_mobile = passwordHash.generate(req.body.mobileUser_pass);
  console.log(hashPass_mobile);
  var save_result
  connection.query("INSERT INTO users_mobile(mobile_name, mobile_age, mobile_carNr, mobile_emergy, mobile_mail, mobile_pass) VALUES('" + req.body.mobileUser_name + "','" + req.body.mobileUser_age + "','" + req.body.mobileUser_carNr + "','" + req.body.mobileUser_emergy + "','" + req.body.mobileUser_mail + "','" + hashPass_mobile + "')", function (err, result2) {
    if (err) throw err
    save_result = result2;
    console.log('The solution is1: ', result2)
    response.send("Register successfully");
  })


});
//  Mobile Register End

//Mobile Login Start
app.post('/api/loginmobile', function (req, response) {
  var save_result
  // console.log(req);
  // console.log("meow " +req.body);
  // console.log("da "+ JSON.stringify(req.body));
  connection.query("SELECT * from users_mobile WHERE mobile_mail='" + req.body.mobileUser_mail + "'", function (err, rows, result2) {
    if (err) throw err
    save_result = result2;

    console.log(rows.length);
    if (rows.length == 1) { //verify personel ID
      var hashedPassword_Mobile = passwordHash.generate(req.body.mobileUser_pass);
      console.log(hashedPassword_Mobile);
      console.log(rows[0].mobile_pass);
      if (rows[0].mobile_mail == req.body.mobileUser_mail && (passwordHash.verify(req.body.mobileUser_pass, rows[0].mobile_pass))) { //verify user and pass
        console.log(rows[0].mobile_mail);
        console.log(rows[0].mobile_pass);
        userData_Mobile = {}; //object
        userData_Mobile.mobile_name = rows[0].mobile_name;
        userData_Mobile.mobile_age = rows[0].mobile_age;
        userData_Mobile.mobile_carNr = rows[0].mobile_carNr;
        userData_Mobile.mobile_emergy = rows[0].mobile_emergy;
        userData_Mobile.mobile_mail = rows[0].mobile_mail; //save data in obj userData
        userData_Mobile.mobile_pass = rows[0].mobile_pass;
        console.log("True");
        response.send(userData_Mobile);
      }
      else {
        response.send("Username or password invalid!")
      }
    }
    else {
      console.log("False");
      response.send("Doesn't exist!");
    }
  });
});

//Mobile Login End

//Mobile User Delete Begin

app.post('/api/mobiledeleteUser', function (req, response) {
  var save_result_user
  connection.query("DELETE FROM users_mobile WHERE mobile_carNr = '" + req.body.mobile_carNr + "'", function (err, resultUser) {
    if (err) throw err
    save_result_user = resultUser;
    console.log('The solution is: ', resultUser)
    response.send("User sters");
  });
});
//Mobile User Delete End

function handleDisconnect() {
  connection = mysql.createConnection({
    host: 'eu-cdbr-west-02.cleardb.net',
    user: 'b9c321f36dafbe',
    password: 'f0d505ac',
    database: 'heroku_4f013f001ff2cdc'
  }) // Recreate the connection, since
  // the old one cannot be reused.

  connection.connect(function (err) {              // The server is either down
    if (err) {                                     // or restarting (takes a while sometimes).
      console.log('error when connecting to db:', err);
      setTimeout(handleDisconnect, 2000); // We introduce a delay before attempting to reconnect,
    }                                     // to avoid a hot loop, and to allow our node script to
  });                                     // process asynchronous requests in the meantime.
  // If you're also serving http, display a 503 error.
  connection.on('error', function (err) {
    console.log("Restarting DB connection...")
    if (err.code === 'PROTOCOL_CONNECTION_LOST') { // Connection to the MySQL server is usually
      handleDisconnect();                         // lost due to either server restart, or a
    } else {                                      // connnection idle timeout (the wait_timeout
      throw err;                                  // server variable configures this)
    }
  });
}

handleDisconnect();