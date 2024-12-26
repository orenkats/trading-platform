using Microsoft.EntityFrameworkCore;
using TransactionsService.Data;
using TransactionsService.Data.Repositories;
using TransactionsService.Logic.EventHandlers;
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
builder.Services.AddDbContext<TransactionDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register Repositories
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Register RabbitMQ EventBus
builder.Services.AddSingleton<IEventBus>(sp => new RabbitMqEventBus(rabbitMqConnection));

// Register Event Handlers
builder.Services.AddScoped<IEventHandler<object>, FundsEventHandler>();

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

// Run the application
var app = builder.Build();
app.Run();
