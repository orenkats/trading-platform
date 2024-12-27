using TransactionsService.Configurations;
using TransactionsService.Extensions;
using TransactionsService.EventConsumers;

var builder = WebApplication.CreateBuilder(args);

// Kestrel configuration
builder.ConfigureKestrel();

// Service registration
builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddRabbitMqConfiguration(builder.Configuration);
builder.Services.AddRepositories();
//builder.Services.AddLogic();
builder.Services.AddEventHandlers();
builder.Services.AddEventHandlers();

// Register Consumers
builder.Services.AddHostedService<FundsEventConsumer>();

var app = builder.Build();
app.Run();
