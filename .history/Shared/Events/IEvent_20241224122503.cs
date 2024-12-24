namespace Shared.Events
{
    public interface IEvent
    {
        Guid EventId { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
