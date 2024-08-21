using TableServe.API.Data;
using TableServe.API.Extensions;
using TableServe.API.Middlewares;
using TableServe.API.Repositories;
using TableServe.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IWaiterRepository, WaiterRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWaiterService, WaiterService>();

var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
DatabaseExtensions.EnsureDatabaseCreated(connectionString);
builder.Services.AddFluentMigrator(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();