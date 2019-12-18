using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace API.Services
{
    public class MedQuery
    {
        public AppDb Db { get; }

        public MedQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<MedService> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`,`Specialty`, `Password` FROM `meds` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<MedService>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name`, `Specialty`, `Password` FROM `meds` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `meds`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<MedService>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<MedService>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new MedService(Db)
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Specialty=reader.GetString(2),
                        Password=reader.GetString(3),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}