using RabbitMQ.Client;
using Shared.Messaging;

namespace PortfolioService.Infrastructure.Configurations
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqUri = configuration.GetSection("RabbitMQ")["Uri"];
            var connectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);
            var connection = connectionFactory.CreateConnection();
            services.AddSingleton<IConnection>(connection);
            services.AddSingleton<IEventBus, RabbitMqEventBus>();

            Console.WriteLine("RabbitMQ connection configured.");
        }
    }
}