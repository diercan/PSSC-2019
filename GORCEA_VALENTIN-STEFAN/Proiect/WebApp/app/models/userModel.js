'use strict';

const log = require('../middlewares/logService');

function UserModel(config, dbService) {
  this._dbService = dbService;
  this._config = config;
}

UserModel.prototype.getUsers = async function (userName) {
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

UserModel.prototype.getUsersWithPassword = async function (pass, userName) {
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

UserModel.prototype.getUser = async function (userName) {
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

module.exports = UserModel;
