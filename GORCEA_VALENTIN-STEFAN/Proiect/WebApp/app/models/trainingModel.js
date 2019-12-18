'use strict';

const log = require('../middlewares/logService');

function TrainingModel(config, dbService) {
  this._dbService = dbService;
  this._config = config;
}

TrainingModel.prototype.getTrainings = async function (userName) {
  return new Promise(async (resolve, reject) => {
    try {
      const sql = `SELECT t.id, t.topic, t.description, u.officialName AS trainer, t.description,
        t.scheduledOn, t.durationHours, t.location, t.maxAttendees, 
        (SELECT count(*) from usertraining ut where ut.trainingId=t.id) as currentAttendees,
        (SELECT GROUP_CONCAT(u.officialName) FROM user u, userTraining ut WHERE u.id=ut.userId AND ut.trainingId = t.id) AS currentAttendeesList,
        (SELECT COUNT(*) FROM user u, userTraining ut WHERE u.id=ut.userId and ut.trainingId = t.id and u.userName=?) AS isCurrentUserAnAttendee
        FROM training t,user u WHERE t.trainerId = u.id AND scheduledOn>now() ORDER BY scheduledOn DESC`;

      const result = await this._dbService.query(sql, [userName]);
      if (result == -1) {
        return resolve([]);
      } else {
        return resolve(result);
      }
    } catch (err) {
      return reject(err);
    }
  });
};

TrainingModel.prototype.joinTraining = async function (trainingId, userName) {
  return new Promise(async (resolve, reject) => {
    try {
      log.debug(`Scheduling meeting for ${userName} for training ${trainingId}`);

      let sql = `SELECT COUNT(*) FROM userTraining ut, training t 
            WHERE ut.trainingId = t.id and t.id=?`;
      let result = await this._dbService.query(sql, [trainingId]);
      sql = `SELECT maxAttendees FROM training where id = ?`;
      let maxAttendeesNumber = await this._dbService.query(sql, [trainingId]);

      if (result >= maxAttendeesNumber.maxAttendees)
        throw new Error("No more seats available!");


      sql = `SELECT t.id, t.topic, u.officialName AS trainer,u.userName as mo,u.password as mp, t.description,
      concat(replace(left(t.scheduledOn,10),'-',''),'T',replace(right(t.scheduledOn,8),':','')) AS scheduledOn, 
      t.durationHours, t.location, t.maxAttendees, 
      (SELECT COUNT(*) FROM usertraining ut WHERE ut.trainingId=t.id) AS currentAttendees  
      FROM training t,user u WHERE t.trainerId = u.id AND t.id=?`;

      result = await this._dbService.query(sql, [trainingId]);
      log.debug(`Training details ${JSON.stringify(result)}`);
      result = result[0];

      sql = `INSERT INTO usertraining (trainingId, userId)  
      SELECT ? as trainingId, id as userId FROM user u 
      WHERE u.userName=?`;

      result = await this._dbService.query(sql, [trainingId, userName]);
      log.debug(`Add user to userTraining : affectedRows=${result.affectedRows}`);

      sql = `SELECT t.id, t.topic, u.officialName AS trainer, t.description,
        t.scheduledOn, t.durationHours, t.location, t.maxAttendees, 
        (SELECT count(*) from usertraining ut where ut.trainingId=t.id) as currentAttendees,
        (SELECT GROUP_CONCAT(u.officialName) FROM user u, userTraining ut WHERE u.id=ut.userId AND ut.trainingId = t.id) AS currentAttendeesList,
        (SELECT COUNT(*) FROM user u, userTraining ut WHERE u.id=ut.userId and ut.trainingId = t.id and u.userName=?) AS isCurrentUserAnAttendee
        FROM training t,user u WHERE t.trainerId = u.id AND scheduledOn>now() ORDER BY scheduledOn DESC`;
      result = await this._dbService.query(sql, [userName]);

      return resolve(result);

    } catch (err) {
      return reject(err);
    }
  });
};

TrainingModel.prototype.createTraining = async function (training, user) {
  return new Promise(async (resolve, reject) => {
    try {
      let sql = `INSERT INTO training(topic,description, location ,scheduledOn,trainerId,maxAttendees,currentAttendees, durationHours) values(?,?,?,?,?,?,?,?)`;
      let result = await this._dbService.query(sql, [training.topic, training.description, training.location, training.date.split("T")[0], user.id, training.seats, 0, training.duration]);
      log.debug(`New Training Was Inserted : affectedRows=${result.affectedRows}`);

      sql = `SELECT t.id, t.topic, u.officialName AS trainer,u.userName as mo,u.password as mp, t.description,
      concat(replace(left(t.scheduledOn,10),'-',''),'T',replace(right(t.scheduledOn,8),':','')) AS scheduledOn, 
      t.durationHours, t.location, t.maxAttendees, 
      (SELECT COUNT(*) FROM usertraining ut WHERE ut.trainingId=t.id) AS currentAttendees  
      FROM training t,user u WHERE t.trainerId = u.id AND t.id=?`;

      result = await this._dbService.query(sql, [result.insertId]);
      result = result[0];

      sql = `UPDATE training SET currentAttendees=currentAttendees-1 WHERE id=?`;
      result = await this._dbService.query(sql, [result.insertId]);
      log.debug(`Update userttraining --attendeess : affectedRows=${result.affectedRows}`);

      sql = `SELECT t.id, t.topic, u.officialName AS trainer, t.description,
        t.scheduledOn, t.durationHours, t.location, t.maxAttendees ,
        (SELECT count(*) from usertraining ut where ut.trainingId=t.id) as currentAttendees,
        (SELECT GROUP_CONCAT(u.officialName) FROM user u, userTraining ut WHERE u.id=ut.userId AND ut.trainingId = t.id) AS currentAttendeesList,
        (SELECT COUNT(*) FROM user u, userTraining ut WHERE u.id=ut.userId and ut.trainingId = t.id and u.userName=?) AS isCurrentUserAnAttendee
        FROM training t,user u WHERE t.trainerId = u.id AND scheduledOn>now() ORDER BY scheduledOn DESC`;
      result = await this._dbService.query(sql, [user.userName]);

      return resolve(result);

    } catch (err) {
      return reject(err);
    }
  });
};


TrainingModel.prototype.cancelJoinTraining = async function (trainingId, userName) {
  return new Promise(async (resolve, reject) => {
    try {

      let sql = `DELETE FROM usertraining WHERE trainingId=? AND 
        userId IN (SELECT id FROM user where userName=?)`;
      let result = await this._dbService.query(sql, [trainingId, userName]);
      log.debug(`Delete user from userttraining : affectedRows=${result.affectedRows}`);

      sql = `SELECT t.id, t.topic, u.officialName AS trainer,u.userName as mo,u.password as mp, t.description,
      concat(replace(left(t.scheduledOn,10),'-',''),'T',replace(right(t.scheduledOn,8),':','')) AS scheduledOn, 
      t.durationHours, t.location, t.maxAttendees, 
      (SELECT COUNT(*) FROM usertraining ut WHERE ut.trainingId=t.id) AS currentAttendees  
      FROM training t,user u WHERE t.trainerId = u.id AND t.id=?`;

      result = await this._dbService.query(sql, [trainingId]);
      result = result[0];

      sql = `UPDATE training SET currentAttendees=currentAttendees-1 WHERE id=?`;
      result = await this._dbService.query(sql, [trainingId]);
      log.debug(`Update userttraining --attendeess : affectedRows=${result.affectedRows}`);

      sql = `SELECT t.id, t.topic, u.officialName AS trainer, t.description,
        t.scheduledOn, t.durationHours, t.location, t.maxAttendees, 
        (SELECT count(*) from usertraining ut where ut.trainingId=t.id) as currentAttendees,
        (SELECT GROUP_CONCAT(u.officialName) FROM user u, userTraining ut WHERE u.id=ut.userId AND ut.trainingId = t.id) AS currentAttendeesList,
        (SELECT COUNT(*) FROM user u, userTraining ut WHERE u.id=ut.userId and ut.trainingId = t.id and u.userName=?) AS isCurrentUserAnAttendee
        FROM training t,user u WHERE t.trainerId = u.id AND scheduledOn>now() ORDER BY scheduledOn DESC`;
      result = await this._dbService.query(sql, [userName]);

      return resolve(result);

    } catch (err) {
      return reject(err);
    }
  });
};

TrainingModel.prototype.deleteTraining = async function (trainingId, userName) {
  return new Promise(async (resolve, reject) => {
    try {
      let sql = `DELETE FROM usertraining WHERE trainingId=?`;
      let result = await this._dbService.query(sql, [trainingId]);
      log.debug(`Delete user from userttraining : affectedRows=${result.affectedRows}`);

      sql = `DELETE FROM training WHERE id=?`;
      result = await this._dbService.query(sql, [trainingId]);
      log.debug(`Delete from training : affectedRows=${result.affectedRows}`);


      sql = `SELECT t.id, t.topic, u.officialName AS trainer, t.description,
        t.scheduledOn, t.durationHours, t.location, t.maxAttendees, 
        (SELECT count(*) from usertraining ut where ut.trainingId=t.id) as currentAttendees,
        (SELECT GROUP_CONCAT(u.officialName) FROM user u, userTraining ut WHERE u.id=ut.userId AND ut.trainingId = t.id) AS currentAttendeesList,
        (SELECT COUNT(*) FROM user u, userTraining ut WHERE u.id=ut.userId and ut.trainingId = t.id and u.userName=?) AS isCurrentUserAnAttendee
        FROM training t,user u WHERE t.trainerId = u.id AND scheduledOn>now() ORDER BY scheduledOn DESC`;
      result = await this._dbService.query(sql, [userName]);

      return resolve(result);

    } catch (err) {
      return reject(err);
    }
  });
};


module.exports = TrainingModel;
