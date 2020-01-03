module.exports=(sequelize,DataTypes)=>{
    const Usersdb = sequelize.define("Usersdb",{
        idusersdb:{
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
        },
        usersdb_name:{
            type: DataTypes.STRING,
            allowNull: false
        },
        usersdb_mail:{
            type: DataTypes.STRING,
            allowNull: false
        },
        usersdb_username:{
            type: DataTypes.STRING,
            allowNull: false
        },
        usersdb_password:{
            type: DataTypes.STRING,
            allowNull: false
        },
        usersdb_personnelID:{
            type: DataTypes.STRING,
            unique:true,
            allowNull: false
        },
        usersdb_company:{
            type: DataTypes.STRING,
            allowNull: false
        }

    })
    return Usersdb
}