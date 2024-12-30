namespace Shared.Events
{
    public class TransactionCreatedEvent
    {
        public Guid UserId { get; set; }
        public Guid PortfolioId { get; set; }
        public string Type { get; set; } = null!; // e.g., "Deposit", "Withdrawal"
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
