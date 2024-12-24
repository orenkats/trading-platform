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

        public void Subscribe<T>(string queueName, Action<T> onMessageReceived) where T : class
        {
            using var channel = _connection.CreateModel();

            // Declare the queue
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);

            // Bind the queue to the exchange
            var exchangeName = "UserExchange"; // Your exchange name
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: true);
            channel.QueueBind(queueName, exchangeName, routingKey: "");

            // Set up a consumer to listen for messages
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<T>(message);
                onMessageReceived(@event!);
            };

            // Start consuming messages from the queue
            channel.BasicConsume(queueName, autoAck: true, consumer: consumer);
        }
    }
}
