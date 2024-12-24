using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Shared.MySQL;
using Shared.Messaging;
using Shared.RabbitMQ;
using UserService.Data.Repositories;
using UserService.Logic;

var builder = WebApplication.CreateBuilder(args);

// Configure MySQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Configure Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure Business Logic
builder.Services.AddScoped<IUserLogic, UserLogic>();

// Configure RabbitMQ Messaging
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
builder.Services.AddSingleton<IEventBus>(new RabbitMqEventBus(new RabbitMqConnectionFactory(rabbitMqUri)));

var app = builder.Build();

// Enable routing
app.UseRouting();

// Map controllers
app.MapControllers();

app.Run();
