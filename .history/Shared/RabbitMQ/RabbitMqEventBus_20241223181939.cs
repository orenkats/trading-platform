using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shared.Messaging;

namespace Shared.RabbitMQ
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;

        // Constructor to initialize RabbitMQ Event Bus
        // Declares the exchange once during initialization
        public RabbitMqEventBus(IConnection connection)
        {
            _connection = connection;

            // Declare the exchange during startup to ensure it exists
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare("UserExchange", ExchangeType.Fanout, durable: true);
        }

        // Publishes an event to the specified exchange
        public void Publish<T>(T @event, string exchangeName) where T : class
        {
            using var channel = _connection.CreateModel();

            // Serialize the event object to a JSON string
            var message = JsonSerializer.Serialize(@event);

            // Convert the JSON string to a byte array
            var body = Encoding.UTF8.GetBytes(message);

            // Publish the message to the specified exchange
            channel.BasicPublish(
                exchange: exchangeName,  // Exchange name
                routingKey: "",          // Fanout exchanges ignore routing keys
                basicProperties: null,   // No custom message properties
                body: body               // The message body
            );
        }
    }
}
