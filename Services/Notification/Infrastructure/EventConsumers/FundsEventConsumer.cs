using RabbitMQ.Client;
using Shared.Messaging;
using Shared.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;

namespace NotificationService.Infrastructure.Consumers
{
    public class FundsEventConsumer : RabbitMqBaseConsumer
    {
        public FundsEventConsumer(
            IConnection connection,
            IMessageProcessor processor,
            IConfiguration configuration)
            : base(connection, processor, configuration)
        {
        }
    }
}
