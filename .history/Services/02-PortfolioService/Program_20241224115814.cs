using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.Data.Repositories;
using PortfolioService.Logic;
using PortfolioService.Logic.EventHandlers;
using Shared.Events;
using Shared.Messaging;
using Shared.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Configure Database
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);

// Register Repositories and Logic
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPortfolioLogic, PortfolioLogic>(); // Ensure this is registered

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
