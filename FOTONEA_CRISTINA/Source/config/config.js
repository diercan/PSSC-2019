const dotenv = require('dotenv')
dotenv.config()

module.exports = {
  port: process.env.PORT || 5000,
  db: {
    database: process.env.DB_NAME || 'heroku_925972ce0eeea60',
    user: process.env.DB_USER || 'b43dd8fd9b0df5',
    password: process.env.DB_PASS || 'b5f7d2b8',
    options: {
      dialect: process.env.DIALECT || 'mysql',
      host: process.env.HOST || 'eu-cdbr-west-02.cleardb.net',
      port: process.env.DB_PORT || '3306'
    }
  }
}