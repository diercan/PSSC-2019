using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace API.Services
{
    public class AppointmentService
    {
        internal AppDb Db { get; set; }
        
        public int Id{ get; set;}
        public string MedName{ get; set; }
        
        public DateTime Date { get; set; }
        public string PacientName { get; set; } 
        public string BreedPet { get; set; } 
        public string Symptoms { get; set; }    
        public AppointmentService()
        {
        }
        public AppointmentService(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `appointments` (`MedName`, `Date`,`PacientName`,`BreedPet`,`Symptoms`) VALUES (@medName, @date,@pacientName,@breedPet,@symptoms);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int) cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `appointments` SET `MedName` = @medName, `Date` = @date, `PacientName` = @pacientName, `BreedPet` = @breedPet, `Symptoms` = @symptoms WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `appointments` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@medName",
                DbType = DbType.String,
                Value = MedName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@date",
                DbType = DbType.DateTime,
                Value = Date,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@pacientName",
                DbType = DbType.String,
                Value = PacientName,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@breedPet",
                DbType = DbType.String,
                Value = BreedPet,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@symptoms",
                DbType = DbType.String,
                Value = Symptoms,
            });
        }
    }
}