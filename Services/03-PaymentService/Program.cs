using PaymentService.Configurations;
using PaymentService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Kestrel configuration
builder.ConfigureKestrel();

// Service registration
builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddRabbitMqConfiguration(builder.Configuration);
builder.Services.AddRepositories();

var app = builder.Build();
app.Run();
