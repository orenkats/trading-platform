using Shared.Messaging.Interfaces;
using Shared.Events;

namespace Shared.Messaging
{
    public class EventResolver : IEventResolver
    {
        private readonly Dictionary<string, Type> _eventTypeMap;

        public EventResolver()
        {
            _eventTypeMap = new Dictionary<string, Type>
            {
                { "PortfolioService_OrderPlacedQueue", typeof(OrderPlacedEvent) },
                { "PortfolioService_UserCreatedQueue", typeof(UserCreatedEvent) },
                // Add more mappings here
            };
        }

        public Type? Resolve(string queueName)
        {
            _eventTypeMap.TryGetValue(queueName, out var eventType);
            return eventType;
        }
    }
}
