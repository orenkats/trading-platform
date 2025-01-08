using System.Threading.Tasks;

namespace Shared.Messaging.Interfaces
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(object @event);
    }
}
