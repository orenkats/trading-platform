
using OrderService.Application.EventHandlers;
using OrderService.Domain.Interfaces;
using OrderService.Domain.Services;
using Shared.Messaging;
using Shared.Events;
using OrderService.Infrastructure.Configurations;
using OrderService.Infrastructure.Consumers;
using OrderService.Infrastructure.Repositories;

namespace OrderService.StartUp
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
            services.AddScoped<IOrderDomainService, OrderDomainService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
        {
            // Register Database Configuration
            DatabaseConfiguration.AddDbConfiguration(services, configuration);

            // Register RabbitMQ Configuration
            RabbitMqConfiguration.AddRabbitMqConfiguration(services, configuration);

            // Register Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Register Event Consumers
            //services.AddHostedService<OrderPlacedEventConsumer>();
            //services.AddHostedService<UserCreatedEventConsumer>();

            // Configure Kestrel
            KestrelConfiguration.ConfigureKestrel(builder);

            return services;
        }
    }
}
