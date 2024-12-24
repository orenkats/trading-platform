namespace Shared.Messaging
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        Task HandleAsync(TEvent @event);
    }
}
