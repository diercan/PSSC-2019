module.exports = (sequelize, DataTypes) => {
    const Event = sequelize.define('Event', {
      id: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true
      },
      title: {
        type: DataTypes.STRING,
        allowNull: false
      },
      description: {
        type: DataTypes.STRING,
        allowNull: false
      },
      startDate : {
        type: DataTypes.DATE,
        allowNull: false,
      },
      endDate : {
        type: DataTypes.DATE,
        allowNull: false,
      }
    })
    Event.associate = function (models)
    {
      Event.belongsTo(models.User, {foreignKey: 'UserId', as: 'User'})
    }
    // User.associate = function (models) {
    //   User.belongsToMany(models.Post, {
    //     through: models.UserPost,
    //     as: 'posts',
    //     foreignKey: 'userId'
    //   });
    //     Candidate.belongsToMany(models.Company, {
    //         through: models.CandidateCompany,
    //         as: 'companies',
    //         foreignKey: 'candidateId'
    //     });
    // };
    return Event
  }
  