namespace TableServe.API.Models;

public class Order
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public int WaiterId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

