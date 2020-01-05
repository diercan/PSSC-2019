const dotenv = require('dotenv')
dotenv.config()

module.exports = {
  port: process.env.PORT || 5000,
  db: {
    database: process.env.DB_NAME || 'heroku_83eaa60d98d05a3',
    user: process.env.DB_USER || 'b07289a8a44ebc',
    password: process.env.DB_PASS || '225c8998',
    options: {
      dialect: process.env.DIALECT || 'mysql',
      host: process.env.HOST || 'eu-cdbr-west-02.cleardb.net',
      port: process.env.DB_PORT || '3306'
    }
  }
}
