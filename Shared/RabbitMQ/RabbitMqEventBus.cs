using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Shared.Messaging;

namespace Shared.RabbitMQ
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqEventBus(RabbitMqConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Publish<T>(T @event, string exchangeName) where T : class
        {
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchangeName, string.Empty, null, body);
        }

        public void Subscribe<T>(string queueName, Func<T, Task> onMessage) where T : class
        {
            _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var @event = JsonSerializer.Deserialize<T>(message);
                    if (@event != null)
                    {
                        await onMessage(@event); // Await the async handler
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            _channel.BasicConsume(queueName, autoAck: true, consumer);
        }
    }
}
