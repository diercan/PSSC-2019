var express = require('express');

var serveStatic = require('serve-static');
const Lumie = require('lumie')
const bodyParser = require('body-parser')
const Morgan = require('morgan')
const path = require('path')
const { sequelize } = require('./Backend/models')
const config = require('./Backend/config/config.js')
const cors = require('cors')
var logging = require(__dirname + "/Backend/logging.js")
var serverAPIs = require(__dirname + "/Backend/serverAPIs.js")

const PORT = config.port

const app = express();
app.use(bodyParser.json())
app.use(Morgan())
app.use(cors());
// app.use((req, res, next) => {
//     res.setHeader('Access-Control-Allow-Origin', '*')
//     res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE')
//     res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type')
//     res.setHeader('Access-Control-Allow-Credentials', true)
//     next()
//   })

  Lumie.load(app, {
    verbose: true, // process.env.NODE_ENV === 'dev'
    preURL: 'api',
    ignore: ['*.spec', '*.action'],
    controllers_path: path.join(__dirname, '/Backend/controllers')
  })

  app.use(express.static(path.join(__dirname, 'Frontend/build')));
app.get('/*', function(req, res) {
  res.sendFile(path.join(__dirname, 'Frontend/build', 'index.html'));
});


sequelize.sync({ logging: false } ) // { force: true } - To reset DB insert this inside the parenthesis
  .then(() => {
    app.listen(process.env.PORT || 5000, () => {
      logging.LOG(__filename, 39,`Server listening on port ${PORT}`)
    })
  })


logging.GOD(`my-events server started on port:${PORT}`)


app.use(express.json());

console.log("static")
// serverDB.connectToDB()
module.exports = app;
