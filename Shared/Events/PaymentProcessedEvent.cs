using System;

namespace Shared.Events
{
    public class PaymentProcessedEvent
    {
        public Guid UserId { get; set; } // ID of the user who initiated the payment
        public Guid PaymentId { get; set; } // ID of the processed payment
        public decimal Amount { get; set; } // Amount of the payment
        public string Status { get; set; } = null!; // Status of the payment, e.g., "Completed" or "Failed"
        public string TransactionType { get; set; } = null!; // Type of transaction: "Deposit" or "Withdrawal"
        public DateTime Timestamp { get; set; } // Timestamp when the payment was processed
    }
}
