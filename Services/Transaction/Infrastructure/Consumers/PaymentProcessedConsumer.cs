using RabbitMQ.Client;
using Shared.Messaging;
using Shared.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;

namespace TransactionService.Infrastructure.Consumers
{
    public class PaymentProcessedConsumer : RabbitMqBaseConsumer
    {
        public PaymentProcessedConsumer(
            IConnection connection,
            IMessageProcessor processor,
            IConfiguration configuration)
            : base(connection, processor, configuration)
        {
        }
    }
}
