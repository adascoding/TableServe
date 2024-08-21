using TableServe.API.Models;

namespace TableServe.API.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Order> CreateOrderAsync(Order order);
}
