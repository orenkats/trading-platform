using RabbitMQ.Client;
using Shared.Messaging;
using Shared.Messaging.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PortfolioService.Infrastructure.Consumers
{
    public class DepositProcessedConsumer : RabbitMqBaseConsumer
    {
        public DepositProcessedConsumer(
            IConnection connection,
            IMessageProcessor processor,
            IConfiguration configuration)
            : base(connection, processor, configuration)
        {
        }
    }
}
