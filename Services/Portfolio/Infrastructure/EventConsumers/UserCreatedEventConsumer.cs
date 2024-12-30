using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;

namespace PortfolioService.Infrastructure.EventConsumers
{
    public class UserCreatedEventConsumer : ConsumerHostedService<UserCreatedEvent>
    {
        public UserCreatedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            ILogger<ConsumerHostedService<object>> logger)
            : base(serviceProvider, connection, new ConsumerHostedServiceOptions
            {
                QueueName = "PortfolioService_UserCreatedQueue"
            }, logger)
        {
        }
    }
}