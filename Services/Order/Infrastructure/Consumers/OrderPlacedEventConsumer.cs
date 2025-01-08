using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure.Consumers
{
    public class OrderPlacedEventConsumer : RabbitMqBaseConsumer<OrderPlacedEvent>
    {
        public OrderPlacedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<RabbitMqBaseConsumer<OrderPlacedEvent>> logger)
            : base(
                serviceProvider: serviceProvider,
                connection: connection,
                configuration: configuration,
                queueName: "OrderService_OrderPlacedQueue",
                logger: logger)
        {
        }
    }
}
