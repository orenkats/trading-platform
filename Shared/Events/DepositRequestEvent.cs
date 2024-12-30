namespace Shared.Events;
public class DepositRequestedEvent
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime RequestedAt { get; set; }
}