
using PortfolioService.Application.Extensions;
using PortfolioService.Domain.Extensions;
using PortfolioService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationLayer()
    .AddDomainLayer()
    .AddInfrastructureLayer(builder.Configuration, builder)
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
