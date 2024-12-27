using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace NotificationsService.EventConsumers
{
    public class FundsEventConsumer : ConsumerHostedService<object>
    {
        public FundsEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "NotificationsService_TransactionsQueue"
            }, logger)
        {
        }
    }
}
