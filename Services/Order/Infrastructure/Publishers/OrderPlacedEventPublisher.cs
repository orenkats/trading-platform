using Shared.Events;
using Shared.Messaging;

namespace OrderService.Infrastructure.Publishers
{
    public class OrderPlacedEventPublisher
    {
        private readonly IEventPublisher _Publisher;

        public OrderPlacedEventPublisher(IEventPublisher eventPublisher)
        {
            _Publisher = eventPublisher;
        }

        public void PublishOrderPlaced(Guid userId, decimal amount, string status)
        {
            var OrderPlacedEvent = new OrderPlacedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                //StockSymbol = stockSymbol,
                Timestamp = DateTime.UtcNow
            };

            _Publisher.Publish(OrderPlacedEvent, "OrderExchange");
        }
    }
}
