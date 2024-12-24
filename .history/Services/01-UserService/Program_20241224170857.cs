using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using Shared.Messaging;
using Shared.RabbitMQ;
using UserService.Data.Repositories;
using UserService.Logic;

var builder = WebApplication.CreateBuilder(args);

// Configure UserDbContext for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Configure Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure Business Logic
builder.Services.AddScoped<IUserLogic, UserLogic>();

// Configure RabbitMQ
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
var connectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);
var rabbitMqConnection = connectionFactory.CreateConnection(); // Create the connection here

builder.Services.AddSingleton<IEventBus>(new RabbitMqEventBus(rabbitMqConnection));

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
