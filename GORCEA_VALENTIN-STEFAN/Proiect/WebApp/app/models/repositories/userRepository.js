'use strict';

const log = require('../../middlewares/logService');

function UserRepository(config, dbService) {
  this._dbService = dbService;
  this._config = config;
}

UserRepository.prototype.getUsers = async function () {
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

UserRepository.prototype.getUsersWithPassword = async function (pass, userName) {
  return new Promise(async (resolve, reject) => {
    try {
      let sql = `SELECT * FROM user where password = ? and userName = ? `;
     
      let result = await this._dbService.query(sql, [pass, userName]);
      
      log.debug(`--->SUCCES: get users details : ${JSON.stringify(result)}`);
      resolve(result);

    } catch (err) {
      return reject(err);
    }
  });
}

UserRepository.prototype.getUser = async function (userName) {
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

module.exports = UserRepository;
