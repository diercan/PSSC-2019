module.exports=(sequelize,DataTypes)=>{
    const Requestdb = sequelize.define("Requestdb",{
        idrequestdb:{
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
        },
        requestdb_name:{
            type: DataTypes.STRING,
            allowNull: false
        },
        requestdb_personnelID:{
            type: DataTypes.STRING,
            allowNull: false
        },
        requestdb_username:{
            type: DataTypes.STRING,
            allowNull: false
        },
        requestdb_startdate:{
            type: DataTypes.DATE,
            allowNull: false
        },
        requestdb_enddate:{
            type: DataTypes.DATE,
            allowNull: false
        },
        requestdb_days:{
            type: DataTypes.INTEGER,
            allowNull: false
        },
        requestdb_code:{
            type: DataTypes.INTEGER,
            allowNull: false
        }
    })
    return Requestdb
}