using Microsoft.Data.Sqlite;

namespace TableServe.API.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqliteConnection");
        }

        public SqliteConnection CreateConnection() => new SqliteConnection(_connectionString);
    }
}
