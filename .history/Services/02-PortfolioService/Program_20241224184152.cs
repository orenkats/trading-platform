using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.Data.Repositories;
using PortfolioService.Logic;
using PortfolioService.Logic.EventHandlers;
using Shared.Events;
using Shared.Messaging;

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
builder.Services.AddSingleton<IEventBus>(new RabbitMqEventBus(rabbitMqConnection));

// Register Event Handlers
builder.Services.AddSingleton<UserCreatedEventHandler>();

var app = builder.Build();

// Subscribe to UserCreatedEvent
var userCreatedEventHandler = app.Services.GetRequiredService<UserCreatedEventHandler>();
var eventConsumer = new RabbitMqEventConsumer(rabbitMqConnection);
eventConsumer.Subscribe<UserCreatedEvent>("UserCreatedQueue", userCreatedEventHandler.HandleUserCreatedEventAsync);

app.Run();
