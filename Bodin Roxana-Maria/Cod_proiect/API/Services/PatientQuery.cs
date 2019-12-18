using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace API.Services
{
    public class PatientQuery
    {
        public AppDb Db { get; }

        public PatientQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<PatientService> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `UserName`,`Password`,`Email`,`Phone` FROM `patients` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        
        public async Task<List<PatientService>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `UserName`,`Password`,`Email`,`Phone`  FROM `patients` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `patients`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<PatientService>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<PatientService>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new PatientService(Db)
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        UserName=reader.GetString(2),
                        Password = reader.GetString(3),
                        Email = reader.GetString(4),
                        Phone=reader.GetString(5),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}