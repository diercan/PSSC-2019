'use strict';

const log = require('./logService');

function UserService(config, dbService) {
  this._dbService = dbService;
  this._config = config;
}

UserService.prototype.getUsers = async function (userName) {
  return new Promise(async (resolve, reject) => {
    try {
      let sql = `SELECT * FROM user`;
     
      let result = await this._dbService.query(sql, []);
      
      log.debug(`--->SUCCES: get users details : ${JSON.stringify(result)}`);
      resolve(result);

    } catch (err) {
      return reject(err);
    }
  });
}

UserService.prototype.getUser = async function (userName) {
  return new Promise(async (resolve, reject) => {
    try {
      let sql = `SELECT * FROM user where userName = ?`;
     
      let result = await this._dbService.query(sql, [userName]);
      
      log.debug(`--->SUCCES: get users details : ${JSON.stringify(result)}`);
      resolve(result);

    } catch (err) {
      return reject(err);
    }
  });
}

module.exports = UserService;
