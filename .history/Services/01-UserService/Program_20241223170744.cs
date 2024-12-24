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

// Configure RabbitMQ Messaging
var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
if (string.IsNullOrEmpty(rabbitMqUri))
{
    Console.WriteLine("Error: RabbitMQ URI is missing. Please check appsettings.json.");
    Environment.Exit(1);
}
builder.Services.AddSingleton<IEventBus>(new RabbitMqEventBus(new RabbitMqConnectionFactory(rabbitMqUri)));

// Add Controller Services
builder.Services.AddControllers();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();