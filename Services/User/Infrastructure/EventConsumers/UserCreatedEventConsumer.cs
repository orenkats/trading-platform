using Shared.Events;
using Shared.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace UserService.Infrastructure.EventConsumers
{
    public class PaymentProcessedEventConsumer : ConsumerHostedService<PaymentProcessedEvent>
    {
        public PaymentProcessedEventConsumer(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            ILogger<ConsumerHostedService<PaymentProcessedEvent>> logger)
            : base(
                serviceProvider: serviceProvider,
                connection: connection,
                configuration: configuration,
                queueName: "UserService_UserCreatedQueue",
                logger: logger)
        {
        }
    }
}
