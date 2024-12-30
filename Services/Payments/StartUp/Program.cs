
using PortfolioService.StartUp;
using PortfolioService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add layers to DI container
builder.Services.AddApplicationLayer();
builder.Services.AddDomainLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration, builder);

builder.Services.AddControllers();
// Configure the HTTP request pipeline
var app = builder.Build();

app.MapControllers();

app.Run();
