using Dapper;
using TableServe.API.Data;
using TableServe.API.Models;

public class WaiterRepository : IWaiterRepository
{
    private readonly DapperContext _context;

    public WaiterRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Waiter>> GetWaitersAsync()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "SELECT * FROM Waiters";
            return await connection.QueryAsync<Waiter>(sql);
        }
    }

    public async Task<Waiter> GetWaiterByIdAsync(int waiterId)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "SELECT * FROM Waiters WHERE WaiterId = @Id";
            return await connection.QueryFirstOrDefaultAsync<Waiter>(sql, new { Id = waiterId });
        }
    }

    public async Task<string> GenerateApiKeyAsync(string waiterName)
    {
        using (var connection = _context.CreateConnection())
        {
            var existingWaiter = await connection.QuerySingleOrDefaultAsync<Waiter>(
                "SELECT * FROM Waiters WHERE Name = @Name",
                new { Name = waiterName });

            if (existingWaiter != null)
            {
                throw new InvalidOperationException("An API key already exists for a waiter with this name.");
            }

            var apiKey = Guid.NewGuid().ToString("N");

            var sql = @"
                INSERT INTO Waiters (Name, ApiKey)
                VALUES (@Name, @ApiKey)";

            await connection.ExecuteAsync(sql, new { Name = waiterName, ApiKey = apiKey });

            return apiKey;
        }
    }

    public async Task<Waiter> GetWaiterByApiKeyAsync(string apiKey)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "SELECT * FROM Waiters WHERE ApiKey = @ApiKey";
            return await connection.QueryFirstOrDefaultAsync<Waiter>(sql, new { ApiKey = apiKey });
        }
    }
}
