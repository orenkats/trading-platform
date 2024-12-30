using PortfolioService.Application.Services;
using PortfolioService.Application.EventHandlers;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Domain.Services;
using Shared.Messaging;
using Shared.Events;
using PortfolioService.Infrastructure.Configurations;
using PortfolioService.Infrastructure.EventConsumers;
using PortfolioService.Infrastructure.Persistence.Repositories;

namespace PortfolioService.StartUp
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Register Application Services
            services.AddScoped<IPortfolioAppService, PortfolioAppService>();

            // Register Event Handlers
            services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
            services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();

            return services;
        }

        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            // Register Domain Services
            services.AddScoped<IPortfolioDomainService, PortfolioDomainService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
        {
            // Register Database Configuration
            DatabaseConfiguration.AddDbConfiguration(services, configuration);

            // Register RabbitMQ Configuration
            RabbitMqConfiguration.AddRabbitMqConfiguration(services, configuration);

            // Register Repositories
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();

            // Register Event Consumers
            services.AddHostedService<OrderPlacedEventConsumer>();
            services.AddHostedService<UserCreatedEventConsumer>();

            // Configure Kestrel
            KestrelConfiguration.ConfigureKestrel(builder);

            return services;
        }
    }
}
