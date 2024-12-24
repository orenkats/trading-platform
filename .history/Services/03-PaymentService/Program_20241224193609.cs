using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Data.Repositories;
using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

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

// Register RabbitMQ
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
var connectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);
var rabbitMqConnection = connectionFactory.CreateConnection();
builder.Services.AddSingleton<IConnection>(rabbitMqConnection);

// Add ConsumerHostedService for OrderPlacedEvent
builder.Services.AddHostedService(sp =>
    new ConsumerHostedService<OrderPlacedEvent>(
        sp,
        sp.GetRequiredService<IConnection>(),
        "OrderPlacedQueue"
    )
);

// Register EventBus
builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();

var app = builder.Build();
app.Run();
