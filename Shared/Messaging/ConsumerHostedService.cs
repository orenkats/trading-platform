using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Shared.Messaging
{
    public class ConsumerHostedService<TEvent> : BackgroundService where TEvent : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly string _queueName;

        public ConsumerHostedService(IServiceProvider serviceProvider, IConnection connection, string queueName)
        {
            _serviceProvider = serviceProvider;
            _connection = connection;
            _queueName = queueName;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connection.CreateModel();

            // Declare the queue if not already present
            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<TEvent>(message);

                if (@event != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var handlerType = typeof(IEventHandler<>).MakeGenericType(typeof(TEvent));
                    var handler = scope.ServiceProvider.GetService(handlerType);

                    if (handler is not null)
                    {
                        var handleMethod = handler.GetType().GetMethod("HandleAsync");
                        if (handleMethod != null)
                        {
                            await (Task)handleMethod.Invoke(handler, new object[] { @event });
                        }
                    }
                }
            };

            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
