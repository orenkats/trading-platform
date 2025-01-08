using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Shared.Messaging
{
    public static class QueueConfiguration
    {
        public static void ConfigureQueues(IModel channel, IConfiguration configuration)
        {
            var queues = configuration.GetSection("RabbitMQ:Queues").Get<List<QueueConfig>>();

            foreach (var queue in queues)
            {
                channel.QueueDeclare(queue: queue.Name, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind(queue: queue.Name, exchange: queue.Exchange, routingKey: queue.RoutingKey);
            }
        }

    }
    
    public class QueueConfig
    {
        public string Name { get; set; } = string.Empty;
        public string Exchange { get; set; } = string.Empty;
        public string RoutingKey { get; set; } = string.Empty;
    }

}
