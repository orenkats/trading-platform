using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class WithdrawFundsCommand
    {
        private readonly IPortfolioDomainService _domainService;

        public WithdrawFundsCommand(IPortfolioDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task ExecuteAsync(Guid userId, decimal amount)
        {
            await _domainService.WithdrawFundsAsync(userId, amount);
        }
    }
}
