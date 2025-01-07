using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PaymentService.Infrastructure.EventConsumers
{
    public class WithdrawalRequestedEventConsumer : ConsumerHostedService<WithdrawalRequestedEvent>
    {
        public WithdrawalRequestedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<ConsumerHostedService<WithdrawalRequestedEvent>> logger)
            : base(
                serviceProvider: serviceProvider,
                connection: connection,
                configuration: configuration,
                queueName: "PaymentService_WithdrawalRequestedQueue",
                logger: logger)
        {
        }
    }
}
