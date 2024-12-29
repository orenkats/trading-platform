namespace PortfolioService.Application.DTOs
{
    public class DepositFundsRequest
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
