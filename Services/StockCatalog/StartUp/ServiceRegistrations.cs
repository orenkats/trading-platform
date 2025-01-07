
//using StockCatalogService.Application.EventHandlers;
using StockCatalogService.Domain.Interfaces;
using StockCatalogService.Domain.Services;
using Shared.Messaging;
using Shared.Events;
using StockCatalogService.Infrastructure.Configurations;
//using StockCatalogService.Infrastructure.EventConsumers;
using StockCatalogService.Infrastructure.Repositories;

namespace StockCatalogService.StartUp
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            
           
            // Register Event Handlers
            //services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
            //services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();

            return services;
        }

        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            // Register Domain Services
            services.AddScoped<IStockCatalogDomainService, StockCatalogDomainService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
        {
            // Register Database Configuration
            DatabaseConfiguration.AddDbConfiguration(services, configuration);

            // Register RabbitMQ Configuration
            RabbitMqConfiguration.AddRabbitMqConfiguration(services, configuration);

            // Register Repositories
            services.AddScoped<IStockCatalogRepository, StockCatalogRepository>();

            // Register Event Consumers
            //services.AddHostedService<OrderPlacedEventConsumer>();
            //services.AddHostedService<UserCreatedEventConsumer>();

            // Configure Kestrel
            KestrelConfiguration.ConfigureKestrel(builder);

            return services;
        }
    }
}
