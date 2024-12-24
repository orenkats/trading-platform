// using Microsoft.Extensions.DependencyInjection; 
// using Microsoft.EntityFrameworkCore; 
// using Microsoft.AspNetCore.Builder;
// using Shared.MySQL; 
// using Shared.RabbitMQ;
// using Microsoft.Extensions.Configuration;

// var builder = WebApplication.CreateBuilder(args);

// // Debug the connection string
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// if (string.IsNullOrEmpty(connectionString))
// {
//     Console.WriteLine("Error: Connection string is null or empty. Please check your appsettings.json file.");
//     return;
// }
// Console.WriteLine($"Using connection string: {connectionString}");

// // Configure MySQL Database
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
// );

// // Configure RabbitMQ
// var rabbitMqUri = builder.Configuration.GetSection("RabbitMQ")["Uri"];
// if (string.IsNullOrEmpty(rabbitMqUri))
// {
//     Console.WriteLine("Error: RabbitMQ URI is null or empty. Please check your appsettings.json file.");
//     return;
// }
// Console.WriteLine($"Using RabbitMQ URI: {rabbitMqUri}");
// var rabbitMqConnectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);

// var app = builder.Build();

// // Test Infrastructure
// using (var scope = app.Services.CreateScope())
// {
//     // Test MySQL Database Connection
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     try
//     {
//         Console.WriteLine("Testing database connection...");
//         var canConnect = dbContext.Database.CanConnect();
//         Console.WriteLine($"Database connection successful: {canConnect}");
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Database connection failed: {ex.Message}");
//     }

//     // Test RabbitMQ Connection
//     try
//     {
//         Console.WriteLine("Testing RabbitMQ connection...");
//         using var connection = rabbitMqConnectionFactory.CreateConnection();
//         using var channel = connection.CreateModel();
//         Console.WriteLine("RabbitMQ connection successful!");
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"RabbitMQ connection failed: {ex.Message}");
//     }
// }

// app.Run();
