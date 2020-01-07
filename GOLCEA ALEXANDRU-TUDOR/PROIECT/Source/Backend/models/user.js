module.exports = (sequelize, DataTypes) => {
    const User = sequelize.define('User', {
      id: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true
      },
      firstName: {
        type: DataTypes.STRING,
        allowNull: false
      },
      lastName: {
        type: DataTypes.STRING,
        allowNull: false
      },
      email : {
        type: DataTypes.STRING,
        allowNull: false,
        unique:true
      },
      password:{
        type: DataTypes.STRING,
        allowNull: false,
      }
    })
    User.associate = function(models) {
      User.hasMany(models.Event,{as:"events"})

    };
     

    return User
  }
  