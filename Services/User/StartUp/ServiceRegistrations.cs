
using TransactionService.Application.EventHandlers;
using TransactionService.Domain.Interfaces;
using TransactionService.Domain.Services;
using Shared.Messaging;
using Shared.Events;
using TransactionService.Infrastructure.Configurations;
using TransactionService.Infrastructure.EventConsumers;
using TransactionService.Infrastructure.Repositories;

namespace TransactionService.StartUp
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
            services.AddScoped<ITransactionDomainService, TransactionDomainService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
        {
            // Register Database Configuration
            DatabaseConfiguration.AddDbConfiguration(services, configuration);

            // Register RabbitMQ Configuration
            RabbitMqConfiguration.AddRabbitMqConfiguration(services, configuration);

            // Register Repositories
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Register Event Consumers
            //services.AddHostedService<OrderPlacedEventConsumer>();
            //services.AddHostedService<UserCreatedEventConsumer>();

            // Configure Kestrel
            KestrelConfiguration.ConfigureKestrel(builder);

            return services;
        }
    }
}
