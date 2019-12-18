using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace API.Services
{
    public class AppointmentQuery
    {
        public AppDb Db { get; }

        public AppointmentQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<AppointmentService> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `MedName`, `Date`, `PacientName` FROM `appointments` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        public async Task<List<AppointmentService>> FindAsyncString(string medName)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `MedName`, `Date`, `PacientName`,`BreedPet`,`Symptoms` FROM `appointments` WHERE `MedName` = @medName";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@medName",
                DbType = DbType.String,
                Value = medName,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }
        public async Task<List<AppointmentService>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `MedName`, `Date`, `PacientName`,`BreedPet`,`Symptoms` FROM `appointments` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `appointments`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<AppointmentService>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<AppointmentService>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new AppointmentService(Db)
                    {
                        Id = reader.GetInt32(0),
                        MedName = reader.GetString(1),
                        Date=reader.GetDateTime(2),
                        PacientName = reader.GetString(3),
                        BreedPet=reader.GetString(4),
                        Symptoms = reader.GetString(5),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}