using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public class DapperContext
    {
        private readonly string _connectionString;
        private readonly DatabaseConfig databaseConfig = null!;

        public DapperContext(DatabaseConfig databaseConfig) => _connectionString = databaseConfig.Name;

        public IDbConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
