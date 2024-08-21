using TableServe.API.Data;
using TableServe.API.Models;
using TableServe.API.Services;

public class WaiterService : IWaiterService
{
    private readonly IWaiterRepository _waiterRepository;

    public WaiterService(IWaiterRepository waiterRepository)
    {
        _waiterRepository = waiterRepository;
    }

    public async Task<IEnumerable<Waiter>> GetWaitersAsync()
    {
        return await _waiterRepository.GetWaitersAsync();
    }

    public async Task<Waiter> GetWaiterByIdAsync(int waiterId)
    {
        return await _waiterRepository.GetWaiterByIdAsync(waiterId);
    }

    public async Task<string> GenerateApiKeyAsync(string waiterName)
    {
        return await _waiterRepository.GenerateApiKeyAsync(waiterName);
    }

    public async Task<Waiter> GetWaiterByApiKeyAsync(string apiKey)
    {
        return await _waiterRepository.GetWaiterByApiKeyAsync(apiKey);
    }
}
