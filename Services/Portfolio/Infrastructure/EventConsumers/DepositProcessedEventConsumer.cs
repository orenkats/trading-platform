using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

namespace PortfolioService.Infrastructure.EventConsumers
{
    public class DepositProcessedEventConsumer : ConsumerHostedService<DepositRequestedEvent>
    {
        public DepositProcessedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "PortfolioService_DepositQueue"
            }, logger)
        {
        }
    }
}
