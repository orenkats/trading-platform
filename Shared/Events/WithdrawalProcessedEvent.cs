namespace Shared.Events
{
    public class WithdrawalProcessedEvent
    {
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Completed"; // "Completed" or "Failed"
        public DateTime Timestamp { get; set; }
    }
}
