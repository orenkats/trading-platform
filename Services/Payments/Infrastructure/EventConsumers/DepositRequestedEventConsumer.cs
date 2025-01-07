using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PaymentService.Infrastructure.EventConsumers
{
    public class DepositRequestedEventConsumer : ConsumerHostedService<DepositRequestedEvent>
    {
        public DepositRequestedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<ConsumerHostedService<DepositRequestedEvent>> logger)
            : base(
                serviceProvider: serviceProvider,
                connection: connection,
                configuration: configuration,
                queueName: "PaymentService_DepositRequestedQueue",
                logger: logger)
        {
        }
    }
}
