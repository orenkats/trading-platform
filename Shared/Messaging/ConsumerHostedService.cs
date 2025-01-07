using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;

namespace Shared.Messaging
{
    public class ConsumerHostedService<TEvent> : BackgroundService where TEvent : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly QueueConfig _queueConfig;
        private readonly ILogger<ConsumerHostedService<TEvent>> _logger;

        public ConsumerHostedService(
            IServiceProvider serviceProvider,
            IConnection connection,
            IConfiguration configuration,
            string queueName,
            ILogger<ConsumerHostedService<TEvent>> logger)
        {
            _serviceProvider = serviceProvider;
            _connection = connection;
            _logger = logger;

            // Retrieve queue configuration dynamically
            var queues = configuration.GetSection("RabbitMQ:Queues").Get<List<QueueConfig>>() 
                         ?? throw new ArgumentNullException("RabbitMQ:Queues configuration is missing or invalid.");

            _queueConfig = queues.FirstOrDefault(q => q.Name == queueName) 
                           ?? throw new ArgumentException($"Queue configuration not found for queue: {queueName}");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting ConsumerHostedService for queue: {QueueName}", _queueConfig.Name);

            var channel = _connection.CreateModel();

            // Declare and bind the queue based on configuration
            channel.QueueDeclare(queue: _queueConfig.Name, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: _queueConfig.Name, exchange: _queueConfig.Exchange, routingKey: _queueConfig.RoutingKey);

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

            // Consume messages from the queue
            channel.BasicConsume(queue: _queueConfig.Name, autoAck: true, consumer: consumer);
            _logger.LogInformation("Started consuming messages from queue: {QueueName}", _queueConfig.Name);

            return Task.CompletedTask;
        }
    }
}
