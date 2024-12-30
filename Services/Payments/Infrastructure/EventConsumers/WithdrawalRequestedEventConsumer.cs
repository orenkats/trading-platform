using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace PaymentService.Infrastructure.EventConsumers
{
    public class WithdrawalRequestedEventConsumer : ConsumerHostedService<WithdrawalRequestedEvent>
    {
        public WithdrawalRequestedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "PaymentService_WithdrawalRequestedQueue"
            }, logger)
        {
        }
    }
}
