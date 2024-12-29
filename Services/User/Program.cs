using UserService.Configurations;
using UserService.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureKestrel();
builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddRabbitMqConfiguration(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddLogic();
// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
