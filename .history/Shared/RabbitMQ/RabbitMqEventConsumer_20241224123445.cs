using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Messaging;

namespace Shared.RabbitMQ
{
    public class RabbitMqEventConsumer : IEventConsumer
    {
        private readonly IConnection _connection;

        public RabbitMqEventConsumer(IConnection connection)
        {
            _connection = connection;
        }

        public void Subscribe<T>(string queueName, Func<T, Task> onMessageReceived) where T : class
        {
            using var channel = _connection.CreateModel();

            // Setup consumer to listen for messages
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<T>(message);
                if (@event != null)
                {
                    await onMessageReceived(@event);
                }
            };

            // Consume messages from the pre-configured queue
            channel.BasicConsume(queueName, autoAck: true, consumer: consumer);
        }
    }
}
