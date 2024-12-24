using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Data.Repositories;
using PaymentService.Logic.EventHandlers;
using Shared.Events;
using Shared.Messaging;
using Shared.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use a different port
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5004); // PaymentService runs on port 5004
});

// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register Repositories
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Register Event Handlers
builder.Services.AddSingleton<OrderPlacedEventHandler>();

// Configure RabbitMQ
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
var connectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);
var rabbitMqConnection = connectionFactory.CreateConnection();
builder.Services.AddSingleton<IEventBus>(new RabbitMqEventBus(rabbitMqConnection));

// Subscribe to OrderPlacedEvent
var app = builder.Build();
var orderPlacedEventHandler = app.Services.GetRequiredService<OrderPlacedEventHandler>();
var eventConsumer = new RabbitMqEventConsumer(rabbitMqConnection);
eventConsumer.Subscribe<OrderPlacedEvent>("OrderPlacedQueue", orderPlacedEventHandler.HandleOrderPlacedEventAsync);

app.Run();
