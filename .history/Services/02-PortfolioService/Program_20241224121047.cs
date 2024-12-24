using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.Data.Repositories;
using PortfolioService.Logic;
using PortfolioService.Logic.EventHandlers;
using Shared.Events;
using Shared.Messaging;
using Shared.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Configure PortfolioDbContext for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is missing or empty.");
}

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
