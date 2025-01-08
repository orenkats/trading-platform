namespace Shared.Messaging.Interfaces
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        Task HandleAsync(TEvent @event);
    }
}
