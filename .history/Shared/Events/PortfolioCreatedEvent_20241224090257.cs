namespace Shared.Events
{
    public class PortfolioCreatedEvent : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid PortfolioId { get; set; }
        public Guid UserId { get; set; }
        public string PortfolioName { get; set; } = null!;
    }
}
