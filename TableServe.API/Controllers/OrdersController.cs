using Microsoft.AspNetCore.Mvc;
using TableServe.API.Models;
using TableServe.API.Services;

namespace TableServe.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetOrdersAsync();
        return Ok(orders);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        if (order == null)
        {
            return BadRequest("Order cannot be null");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdOrder = await _orderService.CreateOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
    }
}
