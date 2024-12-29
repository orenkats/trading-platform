using RabbitMQ.Client;
using Shared.Messaging;

namespace UserService.Configurations
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqUri = configuration.GetSection("RabbitMQ")["Uri"];
            var connectionFactory = new RabbitMqConnectionFactory(rabbitMqUri);
            var rabbitMqConnection = connectionFactory.CreateConnection();
            services.AddSingleton<IConnection>(rabbitMqConnection);
            services.AddSingleton<IEventBus, RabbitMqEventBus>();
        }
    }
}
