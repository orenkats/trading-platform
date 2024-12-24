namespace Shared.Events
{
    public class PortfolioCreatedEvent : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid(); // Unique ID for the event
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp of event creation

        public Guid PortfolioId { get; set; } // ID of the created portfolio
        public Guid UserId { get; set; } // Associated user
    }
}
