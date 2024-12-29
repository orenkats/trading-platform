using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

namespace PortfolioService.Infrastructure.EventConsumers
{
    public class OrderPlacedEventConsumer : ConsumerHostedService<OrderPlacedEvent>
    {
        public OrderPlacedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "PortfolioService_OrderPlacedQueue"
            }, logger)
        {
        }
    }
}
