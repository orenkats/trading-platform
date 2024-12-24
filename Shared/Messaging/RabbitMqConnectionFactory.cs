using RabbitMQ.Client;

namespace Shared.Messaging
{
    public class RabbitMqConnectionFactory
    {
        private readonly string _connectionString;

        // Constructor accepting the RabbitMQ URI as a connection string
        public RabbitMqConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Creates and returns a RabbitMQ connection
        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString)
            };
            return factory.CreateConnection();
        }
    }
}
