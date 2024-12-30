namespace Shared.Events;
public class PaymentProcessedEvent
{
    public Guid UserId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = "Completed"; // Completed or Failed
    public string Type { get; set; } = null!; // "Deposit" or "Withdrawal"
    public DateTime Timestamp { get; set; }
}
