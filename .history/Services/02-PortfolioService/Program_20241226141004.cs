using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.Data.Repositories;
using PortfolioService.Logic.EventHandlers;
using PortfolioService.Logic;
using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on a specific port
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5001); // Ensure this port is unique
});

// Configure PortfolioDbContext for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register Repositories and Logic
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioLogic, PortfolioLogic>();

// Register RabbitMQ
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
var connectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);
var rabbitMqConnection = connectionFactory.CreateConnection();
builder.Services.AddSingleton<IConnection>(rabbitMqConnection);

// Register RabbitMQ EventBus
builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();

// Configure and Register ConsumerHostedService for OrderPlacedEvent
builder.Services.AddHostedService<ConsumerHostedService<OrderPlacedEvent>>(provider =>
{
    var connection = provider.GetRequiredService<IConnection>();
    var options = new ConsumerHostedServiceOptions
    {
        QueueName = "OrderPlacedQueue" // Consume events from OrderPlacedQueue
    };
    return new ConsumerHostedService<OrderPlacedEvent>(provider, connection, options);
});

// Configure and Register ConsumerHostedService for UserCreatedEvent
builder.Services.AddHostedService<ConsumerHostedService<UserCreatedEvent>>(provider =>
{
    var connection = provider.GetRequiredService<IConnection>();
    var options = new ConsumerHostedServiceOptions
    {
        QueueName = "UserCreatedQueue" // Consume events from UserCreatedQueue
    };
    return new ConsumerHostedService<UserCreatedEvent>(provider, connection, options);
});


// Register Event Handlers
builder.Services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();
builder.Services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();

var app = builder.Build();
app.Run();
