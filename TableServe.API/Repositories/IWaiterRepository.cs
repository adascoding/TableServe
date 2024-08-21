using TableServe.API.Models;

namespace TableServe.API.Data;

public interface IWaiterRepository
{
    Task<IEnumerable<Waiter>> GetWaitersAsync();
    Task<Waiter> GetWaiterByIdAsync(int waiterId);
    Task<string> GenerateApiKeyAsync(string waiterName);
    Task<Waiter> GetWaiterByApiKeyAsync(string apiKey);
}
