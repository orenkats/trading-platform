using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

namespace TransactionsService.EventConsumers
{
    public class FundsEventConsumer : ConsumerHostedService<object>
    {
        public FundsEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "TransactionsService_TransactionsQueue"
            }, logger)
        {
        }
    }
}
