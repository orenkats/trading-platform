using PortfolioService.Infrastructure.Configurations;
using PortfolioService.Infrastructure.Consumers;
using PortfolioService.Infrastructure.Repositories;
using PortfolioService.Domain.Interfaces;
using Shared.Messaging;
using Shared.Events;

namespace PortfolioService.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(
            this IServiceCollection services, 
            IConfiguration configuration, 
            WebApplicationBuilder builder)
        {
            // Register Database Configuration
            DatabaseConfiguration.AddDbConfiguration(services, configuration);

            // Register RabbitMQ Configuration
            RabbitMqConfiguration.AddRabbitMqConfiguration(services, configuration);

            // Register Repositories
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();

            // Register Event Consumers (Directly use RabbitMqBaseConsumer)
            services.AddHostedService<RabbitMqBaseConsumer<OrderPlacedEvent>>();
            services.AddHostedService<RabbitMqBaseConsumer<UserCreatedEvent>>();

            // Configure Kestrel
            KestrelConfiguration.ConfigureKestrel(builder);

            return services;
        }
    }
}
