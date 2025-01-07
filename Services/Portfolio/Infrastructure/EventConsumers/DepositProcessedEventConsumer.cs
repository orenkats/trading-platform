using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PortfolioService.Infrastructure.EventConsumers
{
    public class DepositProcessedEventConsumer : ConsumerHostedService<DepositProcessedEvent>
    {
        public DepositProcessedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<ConsumerHostedService<DepositProcessedEvent>> logger)
            : base(
                serviceProvider: serviceProvider,
                connection: connection,
                configuration: configuration,
                queueName: "PortfolioService_DepositProcessedQueue",
                logger: logger)
        {
        }
    }
}
