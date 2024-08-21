using Microsoft.Data.Sqlite;

namespace TableServe.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static void EnsureDatabaseCreated(string connectionString)
        {
            var databaseFilePath = ExtractDatabaseFilePath(connectionString);

            if (!File.Exists(databaseFilePath))
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();
                connection.Close();
            }
        }

        private static string ExtractDatabaseFilePath(string connectionString)
        {
            return connectionString.Split('=')[1].Trim();
        }
    }
}
