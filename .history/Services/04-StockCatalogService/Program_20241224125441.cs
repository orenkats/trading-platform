using Microsoft.EntityFrameworkCore;
using StockCatalogService.Data;
using StockCatalogService.Data.Repositories;
using Shared.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Configure StockCatalogDbContext for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StockCatalogDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register Repository
builder.Services.AddScoped<IStockCatalogRepository, StockCatalogRepository>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
