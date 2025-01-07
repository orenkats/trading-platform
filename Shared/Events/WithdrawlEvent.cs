namespace Shared.Events
{
    public class WithdrawalEvent : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid(); // Unique identifier for the event
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!; // Requested, Processed, Approved, Rejected, etc.
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class WithdrawalRequestedEvent : WithdrawalEvent
    {
        public string Source { get; set; } = null!; // E.g., "WebApp"
    }

    public class WithdrawalProcessedEvent : WithdrawalEvent
    {
        public Guid PaymentId { get; set; }
        public string PaymentStatus { get; set; } = null!; // "Completed" or "Failed"
    }

    public class WithdrawalApprovedEvent : WithdrawalEvent
    {
        public decimal UpdatedBalance { get; set; }
    }

    public class WithdrawalRejectedEvent : WithdrawalEvent
    {
        public string Reason { get; set; } = null!; // Reason for rejection (e.g., "Insufficient Balance")
    }
}
