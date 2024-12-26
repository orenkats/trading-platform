using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Data.Repositories;
using PaymentService.Logic.EventHandlers;
using Shared.Messaging;
using Shared.Events;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use a specific port
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5004); // Ensure unique port
});

// Configure RabbitMQ
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
var rabbitMqConnectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);
var rabbitMqConnection = rabbitMqConnectionFactory.CreateConnection();
// IConnection is resolved by DI for RabbitMQ consumers.
builder.Services.AddSingleton<IConnection>(rabbitMqConnection);

// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register Repositories
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Register RabbitMQ EventBus
builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
builder.Services.AddSingleton<IEventBus>(sp => new RabbitMqEventBus(rabbitMqConnection));
// Register Event Handlers
builder.Services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();

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


// Run the application
var app = builder.Build();
app.Run();
