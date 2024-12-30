namespace PortfolioService.Domain.Events;

public class FundsWithdrawnEvent
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }