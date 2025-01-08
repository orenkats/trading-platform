using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shared.Messaging.Interfaces;

namespace Shared.Messaging
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IEventResolver _eventResolver;
        private readonly IEventDispatcher _eventDispatcher;

        public MessageProcessor(IEventResolver eventResolver, IEventDispatcher eventDispatcher)
        {
            _eventResolver = eventResolver;
            _eventDispatcher = eventDispatcher;
        }

        public async Task ProcessAsync(byte[] rawMessage, string queueName)
        {
            // Decode the raw message
            var message = Encoding.UTF8.GetString(rawMessage);

            // Resolve the event type
            var eventType = _eventResolver.Resolve(queueName);
            if (eventType == null)
            {
                throw new InvalidOperationException($"No event type found for queue '{queueName}'.");
            }

            // Deserialize the event
            var @event = JsonSerializer.Deserialize(message, eventType);
            if (@event == null)
            {
                throw new InvalidOperationException($"Failed to deserialize message for queue '{queueName}'.");
            }

            // Pass the event to the dispatcher
            await _eventDispatcher.DispatchAsync(@event);
        }
    }
}
