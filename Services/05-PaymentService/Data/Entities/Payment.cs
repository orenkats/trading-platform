using System;

namespace PaymentService.Data.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } // Unique identifier for the payment
        public Guid OrderId { get; set; } // Associated Order ID
        public Guid UserId { get; set; } // Associated User ID
        public decimal Amount { get; set; } // Payment amount
        public string Status { get; set; } = "Pending"; // Payment status (Pending, Approved, Failed)
        public DateTime? ApprovedAt { get; set; } // Timestamp for approval (nullable)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Automatically set when the payment is created
    }
}
