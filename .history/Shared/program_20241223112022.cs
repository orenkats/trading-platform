using Microsoft.Extensions.DependencyInjection; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Builder;
using Shared.MySQL; 
using Shared.RabbitMQ;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configure MySQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Configure RabbitMQ
var rabbitMqUri = "amqp://guest:guest@localhost:5672/"; // Replace with your RabbitMQ URI
var rabbitMqConnectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);

var app = builder.Build();

// Test Infrastructure
using (var scope = app.Services.CreateScope())
{
    // Test MySQL Database Connection
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        Console.WriteLine("Testing database connection...");
        var canConnect = dbContext.Database.CanConnect();
        Console.WriteLine($"Database connection successful: {canConnect}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }

    // Test RabbitMQ Connection
    try
    {
        Console.WriteLine("Testing RabbitMQ connection...");
        using var connection = rabbitMqConnectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        Console.WriteLine("RabbitMQ connection successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"RabbitMQ connection failed: {ex.Message}");
    }
}

app.Run();
