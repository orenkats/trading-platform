using Microsoft.EntityFrameworkCore;
using StockCatalogService.Data;
using StockCatalogService.Data.Repositories;
using Shared.RabbitMQ;
using StockCatalogService.Logic;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on a specific port
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5002); 
});
// Configure StockCatalogDbContext for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StockCatalogDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register Repository
builder.Services.AddScoped<IStockCatalogRepository, StockCatalogRepository>();
builder.Services.AddScoped<IStockCatalogLogic, StockCatalogLogic>();
// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
