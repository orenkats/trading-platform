using Microsoft.Extensions.Hosting;
using Shared.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public abstract class ConsumerHostedService : BackgroundService
    {
        private readonly IEventConsumer _eventConsumer;

        protected ConsumerHostedService(IEventConsumer eventConsumer)
        {
            _eventConsumer = eventConsumer;
        }

        protected abstract void ConfigureSubscriptions();

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConfigureSubscriptions();
            return Task.CompletedTask;
        }

        protected void Subscribe<TEvent>(string queueName, Func<TEvent, Task> onMessageReceived) where TEvent : class
        {
            _eventConsumer.Subscribe(queueName, onMessageReceived);
        }
    }
}
