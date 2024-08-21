using TableServe.API.Models;

namespace TableServe.API.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Order> CreateOrderAsync(Order order);
}

