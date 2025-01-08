using Shared.Messaging.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(object @event)
        {
            var eventType = @event.GetType();
            using var scope = _serviceProvider.CreateScope();

            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handler = scope.ServiceProvider.GetService(handlerType);

            if (handler == null)
            {
                throw new InvalidOperationException($"Handler not found for event type '{eventType.Name}'.");
            }

            var handleMethod = handler.GetType().GetMethod("HandleAsync");
            if (handleMethod == null)
            {
                throw new InvalidOperationException($"Handler for '{eventType.Name}' does not implement 'HandleAsync'.");
            }

            await (Task)handleMethod.Invoke(handler, new[] { @event });
        }
    }
}
