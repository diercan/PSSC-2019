//AppDb.cs is a disposable Application Database Object, adapted to read the ConnectionString from the Configuration Object:
using System;
using MySql.Data.MySqlClient;

namespace API
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}