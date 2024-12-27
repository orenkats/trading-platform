using PortfolioService.Configurations;
using PortfolioService.Extensions;
using PortfolioService.EventConsumers;

var builder = WebApplication.CreateBuilder(args);

// Kestrel configuration
builder.ConfigureKestrel();

// Service registration
builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddRabbitMqConfiguration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddLogic();
builder.Services.AddEventHandlers();

// Register Consumers
builder.Services.AddHostedService<OrderPlacedEventConsumer>();
builder.Services.AddHostedService<UserCreatedEventConsumer>();

var app = builder.Build();
app.Run();
