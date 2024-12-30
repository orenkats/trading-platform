namespace Shared.Events
{
    public class WithdrawalRejectedEvent
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime RequestedAt { get; set; }
    }
}
