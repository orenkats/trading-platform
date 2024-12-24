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

        // Constructor to initialize the RabbitMQ connection
        public RabbitMqEventConsumer(IConnection connection)
        {
            _connection = connection;
        }

        public void Subscribe<T>(string queueName, Action<T> onMessageReceived) where T : class
        {
            using var channel = _connection.CreateModel();

            // Declare the queue to ensure it exists
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);

            // Declare the exchange and bind the queue
            var exchangeName = typeof(T).Name + "Exchange";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: true); // Declare exchange
            channel.QueueBind(queueName, exchangeName, routingKey: ""); // Bind the queue to the exchange

            // Setup consumer to listen for messages
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (_, ea) =>
            {
                // Deserialize the incoming message
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<T>(message);

                // Invoke the callback with the deserialized message
                onMessageReceived(@event!);
            };

            // Start consuming messages from the queue
            channel.BasicConsume(queueName, autoAck: true, consumer: consumer);
        }
    }
}
