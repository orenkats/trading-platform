using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationsService.Data.Repositories;
using NotificationsService.Logic;
using Shared.Messaging;
using Shared.Events;
using NotificationsService.Logic.EventHandlers;
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

// Configure and Register ConsumerHostedService for a single queue
builder.Services.AddHostedService<ConsumerHostedService<object>>(provider =>
{
    var connection = provider.GetRequiredService<IConnection>();
    var options = new ConsumerHostedServiceOptions
    {
        QueueName = "FundsQueue" // Unified queue for deposit and withdrawal events
    };
    return new ConsumerHostedService<object>(provider, connection, options);
});

// Configure and Register ConsumerHostedService
builder.Services.AddHostedService<ConsumerHostedService<UserCreatedEvent>>(provider =>
{
    var connection = provider.GetRequiredService<IConnection>();
    var options = new ConsumerHostedServiceOptions
    {
        QueueName = "UserCreatedQueue"
    };
    return new ConsumerHostedService<UserCreatedEvent>(provider, connection, options);
});

// Configure Web API
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
