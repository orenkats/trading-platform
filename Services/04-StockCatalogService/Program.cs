using StockCatalogService.Configurations;
using StockCatalogService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on a specific port
builder.ConfigureKestrel();

builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddLogic();
// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
