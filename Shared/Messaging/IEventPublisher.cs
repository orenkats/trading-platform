namespace Shared.Messaging
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event, string exchangeName) where T : class;
    }
}
