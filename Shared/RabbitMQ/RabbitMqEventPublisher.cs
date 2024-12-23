using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Shared.Messaging;

namespace Shared.RabbitMQ
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqEventPublisher(RabbitMqConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public async Task PublishAsync<T>(T @event, string exchangeName) where T : class
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: exchangeName, routingKey: string.Empty, basicProperties: null, body: body);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
