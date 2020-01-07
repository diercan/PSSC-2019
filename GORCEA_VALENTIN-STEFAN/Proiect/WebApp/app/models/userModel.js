'use strict';

function UserModel(id, userName, email, officialName, isTrainer, password) {
  this.id = id;
  this.userName = userName;
  this.email = email;
  this.officialName = officialName;
  this.isTrainer = isTrainer;
  this.password = password;
}

module.exports = UserModel;
