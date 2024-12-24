using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared.MySQL;
using Shared.Messaging;
using Shared.RabbitMQ;
using UserService.Data.Repositories;
using UserService.Logic;

var builder = WebApplication.CreateBuilder(args);

// Configure MySQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Error: Connection string is missing. Please check appsettings.json.");
    Environment.Exit(1);
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
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

var app = builder.Build();

// Enable Routing and Map Controllers
app.UseRouting();
app.MapControllers();

app.Run();
