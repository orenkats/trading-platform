using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.Data.Repositories;
using PortfolioService.Logic;
using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on a specific port
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5001); // Change the port to avoid conflict
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

// Add ConsumerHostedService for UserCreatedEvent
builder.Services.AddHostedService(sp =>
    new ConsumerHostedService<UserCreatedEvent>(
        sp,
        sp.GetRequiredService<IConnection>(),
        "UserCreatedQueue"
    )
);

// Register EventBus
builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();

var app = builder.Build();
app.Run();
