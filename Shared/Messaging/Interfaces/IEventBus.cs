namespace Shared.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T @event, string exchangeName) where T : class;
    }
}
