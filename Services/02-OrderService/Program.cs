using Shared.MySQL;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the MySQL DbContext using the shared helper
builder.Services.AddMySQLDbContext(connectionString);

var app = builder.Build();

app.Run();
