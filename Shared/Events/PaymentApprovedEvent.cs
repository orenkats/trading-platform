namespace Shared.Events
{
    public class PaymentApprovedEvent : IEvent
    {
        public Guid EventId { get; set; } = Guid.NewGuid(); // Unique ID for the event
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp of event creation
        public Guid OrderId { get; set; } // ID of the order
        public Guid UserId { get; set; } // ID of the user
        public decimal Amount { get; set; } // Payment amount
        public string Status { get; set; } = "Approved"; // Payment status
        
    }
}
