using RabbitMQ.Client;

namespace Shared.Messaging
{
    public static class QueueConfiguration
    {
        public static void ConfigureQueue(IModel channel, string queueName)
        {
            switch (queueName)
            {
                case "PortfolioService_UserCreatedQueue":
                    channel.QueueDeclare(queue: "PortfolioService_UserCreatedQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "PortfolioService_UserCreatedQueue", exchange: "UserExchange", routingKey: "");
                    break;

                case "PortfolioService_OrderPlacedQueue":
                    channel.QueueDeclare(queue: "PortfolioService_OrderPlacedQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "PortfolioService_OrderPlacedQueue", exchange: "OrderExchange", routingKey: "");
                    break;    

                case "NotificationsService_UserCreatedQueue":
                    channel.QueueDeclare(queue: "NotificationsService_UserCreatedQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "NotificationsService_UserCreatedQueue", exchange: "UserExchange", routingKey: "");
                    break;

                case "NotificationsService_TransactionsQueue":
                    channel.QueueDeclare(queue: "NotificationsService_TransactionsQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "NotificationsService_TransactionsQueue", exchange: "PortfolioExchange", routingKey: "");
                    break;  

                case "TransactionsService_TransactionsQueue":
                    channel.QueueDeclare(queue: "TransactionsService_TransactionsQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(queue: "TransactionsService_TransactionsQueue", exchange: "PortfolioExchange", routingKey: "");
                    break;        

                // Add more cases here for additional queues
                default:
                    throw new ArgumentException($"Unknown queue: {queueName}");
            }
        }
    }
}
