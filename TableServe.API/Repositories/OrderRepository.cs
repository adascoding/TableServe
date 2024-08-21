using Microsoft.Data.Sqlite;
using System.Data;
using Dapper;
using TableServe.API.Data;
using TableServe.API.Models;

namespace TableServe.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration, DapperContext context)
        {
            _connectionString = configuration.GetConnectionString("SqliteConnection");
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var query = "SELECT * FROM Orders";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Order>(query);
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var query = "SELECT * FROM Orders WHERE OrderId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var order = await connection.QuerySingleOrDefaultAsync<Order>(query, new { Id = orderId });

                if (order != null)
                {
                    var orderItemsSql = "SELECT * FROM OrderItems WHERE OrderId = @Id";
                    order.Items = (await connection.QueryAsync<OrderItem>(orderItemsSql, new { Id = orderId })).ToList();
                }

                return order;
            }
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var orderSql = @"
                INSERT INTO Orders (TableId, WaiterId, OrderDate)
                VALUES (@TableId, @WaiterId, @OrderDate);
                SELECT last_insert_rowid();";

            var parameters = new DynamicParameters();
            parameters.Add("TableId", order.TableId, DbType.Int32);
            parameters.Add("WaiterId", order.WaiterId, DbType.Int32);
            parameters.Add("OrderDate", order.OrderDate, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var orderId = await connection.QuerySingleAsync<int>(orderSql, parameters);

                foreach (var item in order.Items)
                {
                    var itemSql = @"
                        INSERT INTO OrderItems (OrderId, MenuItemId, Quantity)
                        VALUES (@OrderId, @MenuItemId, @Quantity)";

                    var itemParameters = new DynamicParameters();
                    itemParameters.Add("OrderId", orderId, DbType.Int32);
                    itemParameters.Add("MenuItemId", item.MenuItemId, DbType.Int32);
                    itemParameters.Add("Quantity", item.Quantity, DbType.Int32);

                    await connection.ExecuteAsync(itemSql, itemParameters);
                }

                order.OrderId = orderId;
                return order;
            }
        }
    }
}
