using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace PaymentService.Infrastructure.EventConsumers
{
    public class DepositRequestedEventConsumer : ConsumerHostedService<DepositRequestedEvent>
    {
        public DepositRequestedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "PaymentService_DepositRequestedQueue"
            }, logger)
        {
        }
    }
}
