using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationsService.Data.Repositories;
using NotificationsService.Logic;
using Shared.Messaging;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Configure RabbitMQ
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
var rabbitMqConnectionFactory = new ConnectionFactory { Uri = new Uri(rabbitMqUri) };
var rabbitMqConnection = rabbitMqConnectionFactory.CreateConnection();
builder.Services.AddSingleton<IConnection>(rabbitMqConnection);

// Register Repositories
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Register Logic
builder.Services.AddScoped<INotificationLogic, NotificationLogic>();

// Register Event Handlers
builder.Services.AddScoped<IEventHandler<FundsDepositedEvent>, FundsEventHandler>();
builder.Services.AddScoped<IEventHandler<FundsWithdrawnEvent>, FundsEventHandler>();

// Configure Consumer Hosted Service
builder.Services.AddHostedService<ConsumerHostedService<FundsDepositedEvent>>(provider =>
{
    var connection = provider.GetRequiredService<IConnection>();
    var options = new ConsumerHostedServiceOptions { QueueName = "FundsEventsQueue" };
    return new ConsumerHostedService<FundsDepositedEvent>(provider, connection, options);
});

// Configure Web API
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
