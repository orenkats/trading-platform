namespace Shared.Events
{
    public class DepositEvent : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid(); // Unique identifier for the event
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!; // Requested, Processed, Completed, etc.
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class DepositRequestedEvent : DepositEvent
    {
        public string Source { get; set; } = null!; // E.g., "WebApp"
    }

    public class DepositProcessedEvent : DepositEvent
    {
        public Guid PaymentId { get; set; }
        public string PaymentStatus { get; set; } = null!; // "Completed" or "Failed"
    }

    public class DepositCompletedEvent : DepositEvent
    {
        public decimal UpdatedBalance { get; set; }
    }
}
