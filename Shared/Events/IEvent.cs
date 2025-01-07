namespace Shared.Events
{
    public interface IEvent
    {
        Guid EventId { get; set; }
        DateTime Timestamp { get; set; }
    }
}
