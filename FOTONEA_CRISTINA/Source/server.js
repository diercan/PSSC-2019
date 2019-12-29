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
  insertpeople(req.body.name, req.body.mail, req.body.username, req.body.password, req.body.personnelID, req.body.company);
});

function insertpeople(name, mail, username, password, personnelID, company) {
  var hashPass = passwordHash.generate(password);
  console.log(hashPass);
  var save_result
  connection.query("INSERT INTO usersdb(usersdb_name,usersdb_mail,usersdb_username,usersdb_password,usersdb_personnelID,usersdb_company) VALUES('" + name + "','" + mail + "','" + username + "','" + hashPass + "','" + personnelID + "','" + company + "')", function (err, result2) {
    if (err) throw err
    save_result = result2;
    console.log('The solution is1: ', result2)
    console.log(name);
  })

}
// Register WEB End 


// Login WEB Begin
app.post('/api/login', function (req, response) {
  var save_result
  connection.query("SELECT * from usersdb WHERE usersdb_personnelID='" + req.body.personnelID + "'", function (err, rows, result2) {
    if (err) throw err
    save_result = result2;
    console.log(req.body.name);
    console.log(rows.length);
    if (rows.length == 1) { //verify personel ID
      var hashedPassword = passwordHash.generate(req.body.password);
      console.log(hashedPassword);
      console.log(rows[0].usersdb_password);
      if (rows[0].usersdb_username == req.body.username && (passwordHash.verify(req.body.password, rows[0].usersdb_password))) { //verify user and pass
        console.log(rows[0].usersdb_username);
        console.log(rows[0].usersdb_password);
        userData = {}; //object
        userData.username = rows[0].usersdb_username; //save data in obj userData
        userData.password = rows[0].usersdb_password;
        userData.personnelID = rows[0].usersdb_personnelID;
        userData.name = rows[0].usersdb_name;
        userData.mail = rows[0].usersdb_mail;
        userData.company = rows[0].usersdb_company;
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

function handleDisconnect() {
  connection = mysql.createConnection({
    host: 'eu-cdbr-west-02.cleardb.net',
    user: 'b43dd8fd9b0df5',
    password: 'b5f7d2b8',
    database: 'heroku_925972ce0eeea60'
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