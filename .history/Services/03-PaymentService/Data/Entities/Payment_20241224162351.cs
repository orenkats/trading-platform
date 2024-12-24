using System;

namespace PaymentService.Data.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } // Unique identifier for the payment
        public Guid OrderId { get; set; } // Associated Order ID
        public decimal Amount { get; set; } // Payment amount
        public string Status { get; set; } = "Pending"; // Payment status (Pending, Completed, Failed)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for payment creation
    }
}
