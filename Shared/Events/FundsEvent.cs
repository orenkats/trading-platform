namespace Shared.Events
{
    public class FundsDepositedEvent
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class FundsWithdrawnEvent
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
