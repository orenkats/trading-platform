using OrderService.Configurations;
using OrderService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on a specific port
builder.ConfigureKestrel();

// Service registration
builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddRabbitMqConfiguration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddHttpClient();
builder.Services.AddLogic();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
