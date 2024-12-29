namespace PortfolioService.Application.DTOs
{
    public class WithdrawFundsRequest
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
