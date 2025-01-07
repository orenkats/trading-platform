using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure.EventConsumers
{
    public class OrderPlacedEventConsumer : ConsumerHostedService<OrderPlacedEvent>
    {
        public OrderPlacedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<ConsumerHostedService<OrderPlacedEvent>> logger)
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
