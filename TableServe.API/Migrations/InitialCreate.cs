using FluentMigrator;

namespace TableServe.API.Migrations
{
    [Migration(1)]
    public class InitializeDatabase : Migration
    {
        public override void Up()
        {
            Create.Table("Waiters")
                .WithColumn("WaiterId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ApiKey").AsString().NotNullable();

            Create.Table("Tables")
                .WithColumn("TableId").AsInt32().PrimaryKey().Identity()
                .WithColumn("TableNumber").AsString().NotNullable();

            Create.Table("MenuItems")
                .WithColumn("MenuItemId").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Price").AsDecimal().NotNullable();

            Create.Table("Orders")
                .WithColumn("OrderId").AsInt32().PrimaryKey().Identity()
                .WithColumn("TableId").AsInt32().NotNullable().ForeignKey("Tables", "TableId")
                .WithColumn("WaiterId").AsInt32().NotNullable().ForeignKey("Waiters", "WaiterId")
                .WithColumn("OrderDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

            Create.Table("OrderItems")
                .WithColumn("OrderItemId").AsInt32().PrimaryKey().Identity()
                .WithColumn("OrderId").AsInt32().NotNullable().ForeignKey("Orders", "OrderId")
                .WithColumn("MenuItemId").AsInt32().NotNullable().ForeignKey("MenuItems", "MenuItemId")
                .WithColumn("Quantity").AsInt32().NotNullable();

            Insert.IntoTable("Waiters").Row(new { Name = "John Doe", ApiKey = "api_key_1" });
            Insert.IntoTable("Waiters").Row(new { Name = "Jane Smith", ApiKey = "api_key_2" });

            Insert.IntoTable("Tables").Row(new { TableNumber = "Table 1" });
            Insert.IntoTable("Tables").Row(new { TableNumber = "Table 2" });

            Insert.IntoTable("MenuItems").Row(new { Name = "Burger", Price = 5.99m });
            Insert.IntoTable("MenuItems").Row(new { Name = "Pizza", Price = 8.99m });
            Insert.IntoTable("MenuItems").Row(new { Name = "Salad", Price = 4.99m });

            Insert.IntoTable("Orders").Row(new { TableId = 1, WaiterId = 1, OrderDate = "2024-08-20T15:37:25" });
            Insert.IntoTable("Orders").Row(new { TableId = 2, WaiterId = 2, OrderDate = "2024-08-20T15:37:25" });

            Insert.IntoTable("OrderItems").Row(new { OrderId = 1, MenuItemId = 1, Quantity = 2 });
            Insert.IntoTable("OrderItems").Row(new { OrderId = 1, MenuItemId = 3, Quantity = 1 });
            Insert.IntoTable("OrderItems").Row(new { OrderId = 2, MenuItemId = 2, Quantity = 1 });
        }

        public override void Down()
        {
            Delete.Table("OrderItems");
            Delete.Table("Orders");
            Delete.Table("MenuItems");
            Delete.Table("Tables");
            Delete.Table("Waiters");
        }
    }
}
