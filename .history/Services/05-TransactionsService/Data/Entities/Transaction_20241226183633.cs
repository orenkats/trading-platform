using System;

namespace TransactionsService.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }  // Unique transaction ID
        public Guid UserId { get; set; }  // Associated user ID
        public string Type { get; set; } = null!;  // "Deposit", "Withdrawal", "Order"
        public decimal Amount { get; set; }  // The amount of funds for the transaction
        public DateTime Timestamp { get; set; }  // When the transaction happened
        public string Status { get; set; } = null!; // "Pending", "Completed", "Failed"
    }
}
