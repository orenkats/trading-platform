using System;

namespace TransactionsService.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? OrderId { get; set; }
        public string Type { get; set; }
        public decimal? Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
