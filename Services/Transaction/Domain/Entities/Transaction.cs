namespace TransactionService.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = null!; // Deposit or Withdrawal
        public string Status { get; set; } = null!; // Completed, Rejected, etc.
        public DateTime Timestamp { get; set; }
    }
}
