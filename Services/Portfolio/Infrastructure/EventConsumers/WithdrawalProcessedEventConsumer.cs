using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

namespace PortfolioService.Infrastructure.EventConsumers
{
    public class WithdrawalProcessedEventConsumer : ConsumerHostedService<WithdrawalRequestedEvent>
    {
        public WithdrawalProcessedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "PortfolioService_WithdrawalQueue"
            }, logger)
        {
        }
    }
}
