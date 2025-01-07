using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PortfolioService.Infrastructure.Consumers
{
    public class WithdrawalProcessedEventConsumer : ConsumerHostedService<WithdrawalProcessedEvent>
    {
        public WithdrawalProcessedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<ConsumerHostedService<WithdrawalProcessedEvent>> logger)
            : base(
                serviceProvider: serviceProvider,
                connection: connection,
                configuration: configuration,
                queueName: "PortfolioService_WithdrawalProcessedQueue",
                logger: logger)
        {
        }
    }
}
