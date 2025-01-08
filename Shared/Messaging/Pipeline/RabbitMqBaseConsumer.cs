using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Shared.Messaging.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared.Messaging
{
    public class RabbitMqBaseConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IMessageProcessor _processor;
        private readonly IConfiguration _configuration;

        public RabbitMqBaseConsumer(IConnection connection, IMessageProcessor processor, IConfiguration configuration)
        {
            _connection = connection;
            _processor = processor;
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connection.CreateModel();

            // Use QueueConfiguration to dynamically configure queues
            QueueConfiguration.ConfigureQueues(channel, _configuration);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (_, args) =>
            {
                try
                {
                    await _processor.ProcessAsync(args.Body.ToArray(), args.ConsumerTag);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in RabbitMqBaseConsumer: {ex.Message}");
                }
            };

            // Dynamically consume all configured queues
            var queues = _configuration.GetSection("RabbitMQ:Queues").Get<List<QueueConfig>>();
            foreach (var queue in queues)
            {
                channel.BasicConsume(queue.Name, true, consumer);
            }

            return Task.CompletedTask;
        }
    }
}
