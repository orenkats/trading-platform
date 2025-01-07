using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class DepositFundsCommand
    {
        private readonly IPortfolioDomainService _domainService;

        public DepositFundsCommand(IPortfolioDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task ExecuteAsync(Guid userId, decimal amount)
        {
            await _domainService.DepositFundsAsync(userId, amount);
        }
    }
}
