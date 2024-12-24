using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shared.Messaging;

namespace Shared.RabbitMQ
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;

        public RabbitMqEventBus(IConnection connection)
        {
            _connection = connection;
        }

        public void Publish<T>(T @event, string exchangeName) where T : class
        {
            using var channel = _connection.CreateModel();

            // Declare the exchange
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: true);

            // Serialize the event to JSON
            var message = JsonSerializer.Serialize(@event);

            // Convert the message to bytes
            var body = Encoding.UTF8.GetBytes(message);

            // Publish the message to the exchange
            channel.BasicPublish(exchangeName, routingKey: "", basicProperties: null, body: body);
        }
    }
}
