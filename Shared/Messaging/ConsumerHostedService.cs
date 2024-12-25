using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared.Messaging
{
    public class ConsumerHostedService<TEvent> : BackgroundService where TEvent : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly string _queueName;

        public ConsumerHostedService(IServiceProvider serviceProvider, IConnection connection, ConsumerHostedServiceOptions options)
        {
            _serviceProvider = serviceProvider;
            _connection = connection;
            _queueName = options.QueueName ?? throw new ArgumentNullException(nameof(options.QueueName));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                TEvent? @event = JsonSerializer.Deserialize<TEvent>(message);

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
                            var task = handleMethod.Invoke(handler, new object[] { @event }) as Task;
                            if (task == null)
                            {
                                throw new InvalidOperationException($"The method {handleMethod.Name} did not return a Task.");
                            }

                            await task;
                        }
                    }
                }
            };

            // Consume messages from the existing queue
            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }

    public class ConsumerHostedServiceOptions
    {
        public string? QueueName { get; set; }
    }
}
