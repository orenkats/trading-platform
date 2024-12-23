using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Shared.Messaging;

namespace Shared.RabbitMQ
{
    public class RabbitMqEventConsumer : IEventConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqEventConsumer(RabbitMqConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Subscribe<T>(string queueName, Func<T, Task> onMessage) where T : class
        {
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var data = JsonSerializer.Deserialize<T>(message);
                    if (data != null)
                    {
                        await onMessage(data);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
