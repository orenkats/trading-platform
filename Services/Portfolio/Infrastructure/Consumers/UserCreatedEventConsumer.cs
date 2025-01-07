using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace PortfolioService.Infrastructure.Consumers
{
    public class UserCreatedEventConsumer : ConsumerHostedService<UserCreatedEvent>
    {
        public UserCreatedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<ConsumerHostedService<UserCreatedEvent>> logger)
            : base(
                serviceProvider: serviceProvider,
                connection: connection,
                configuration: configuration,
                queueName: "PortfolioService_UserCreatedQueue",
                logger: logger)
        {
        }
    }
}
