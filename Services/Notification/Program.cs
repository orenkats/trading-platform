using NotificationsService.Configurations;
using NotificationsService.Extensions;
using NotificationsService.EventConsumers;

var builder = WebApplication.CreateBuilder(args);

// Kestrel configuration
builder.ConfigureKestrel();

// Service registration

builder.Services.AddRabbitMqConfiguration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddLogic();
builder.Services.AddEventHandlers();

// Register Consumers
builder.Services.AddHostedService<UserCreatedEventConsumer>();
builder.Services.AddHostedService<FundsEventConsumer>();


var app = builder.Build();
app.Run();
