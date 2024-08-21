using TableServe.API.Models;
using TableServe.API.Repositories;

namespace TableServe.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _orderRepository.GetOrdersAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        return await _orderRepository.CreateOrderAsync(order);
    }
}
