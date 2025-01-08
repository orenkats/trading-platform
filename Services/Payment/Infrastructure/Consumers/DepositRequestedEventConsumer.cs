using RabbitMQ.Client;
using Shared.Messaging;
using Shared.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PaymentService.Infrastructure.Consumers
{
    public class DepositRequestedConsumer : RabbitMqBaseConsumer
    {
        public DepositRequestedConsumer(
            IConnection connection,
            IMessageProcessor processor,
            IConfiguration configuration)
            : base(connection, processor, configuration)
        {
        }
    }
}
