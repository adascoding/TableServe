using TableServe.API.Models;

namespace TableServe.API.Services;

public interface IWaiterService
{
    Task<IEnumerable<Waiter>> GetWaitersAsync();
    Task<Waiter> GetWaiterByIdAsync(int waiterId);
    Task<string> GenerateApiKeyAsync(string waiterName);
    Task<Waiter> GetWaiterByApiKeyAsync(string apiKey);
}
