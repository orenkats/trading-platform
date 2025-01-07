using PaymentService.Application.EventHandlers;
using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Services;
using Shared.Messaging;
using Shared.Events;
using PaymentService.Infrastracture.Configurations;
using PaymentService.Infrastructure.Consumers;
using PaymentService.Infrastructure.Repositories;

namespace PaymentService.StartUp
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Register Application Services
            

            // Register Event Handlers
            //services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
            //services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEventHandler>();

            return services;
        }

        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            // Register Domain Services
            services.AddScoped<IPaymentDomainService, PaymentDomainService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
        {
            // Register Database Configuration
            DatabaseConfiguration.AddDbConfiguration(services, configuration);

            // Register RabbitMQ Configuration
            RabbitMqConfiguration.AddRabbitMqConfiguration(services, configuration);

            // Register Repositories
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            // Register Event Consumers
            //services.AddHostedService<OrderPlacedEventConsumer>();
            //services.AddHostedService<UserCreatedEventConsumer>();

            // Configure Kestrel
            KestrelConfiguration.ConfigureKestrel(builder);

            return services;
        }
    }
}
