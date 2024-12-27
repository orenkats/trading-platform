using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared.Messaging
{
    public class ConsumerHostedService<TEvent> : BackgroundService where TEvent : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly string _queueName;
        private readonly ILogger<ConsumerHostedService<object>> _logger;

        public ConsumerHostedService(
            IServiceProvider serviceProvider,
            IConnection connection,
            ConsumerHostedServiceOptions options,
            ILogger<ConsumerHostedService<object>> logger)
        {
            _serviceProvider = serviceProvider;
            _connection = connection;
            _queueName = options.QueueName ?? throw new ArgumentNullException(nameof(options.QueueName));
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting ConsumerHostedService for queue: {QueueName}", _queueName);

            var channel = _connection.CreateModel();

            // Use the QueueConfiguration utility
            QueueConfiguration.ConfigureQueue(channel, _queueName);

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

                    if (handler != null)
                    {
                        try
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
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error occurred while handling event of type {EventType}", typeof(TEvent).Name);
                        }
                    }
                }
            };

            // Consume messages from the existing queue
            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            _logger.LogInformation("Started consuming messages from queue: {QueueName}", _queueName);

            return Task.CompletedTask;
        }
    }

    public class ConsumerHostedServiceOptions
    {
        public string? QueueName { get; set; }
    }
}
