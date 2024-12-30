namespace Shared.Events;
public class WithdrawalRequestedEvent
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime RequestedAt { get; set; }
}
