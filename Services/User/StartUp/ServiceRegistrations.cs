
using UserService.Application.EventHandlers;
using UserService.Domain.Interfaces;
using UserService.Domain.Services;
using Shared.Messaging;
using Shared.Events;
using UserService.Infrastructure.Configurations;
using UserService.Infrastructure.Consumers;
using UserService.Infrastructure.Repositories;

namespace UserService.StartUp
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
            services.AddScoped<IUserDomainService, UserDomainService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
        {
            // Register Database Configuration
            DatabaseConfiguration.AddDbConfiguration(services, configuration);

            // Register RabbitMQ Configuration
            RabbitMqConfiguration.AddRabbitMqConfiguration(services, configuration);

            // Register Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Register Event Consumers
            //services.AddHostedService<OrderPlacedEventConsumer>();
            //services.AddHostedService<UserCreatedEventConsumer>();

            // Configure Kestrel
            KestrelConfiguration.ConfigureKestrel(builder);

            return services;
        }
    }
}
