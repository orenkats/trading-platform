using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class DepositFundsCommand
    {
        private readonly Guid _userId;
        private readonly decimal _amount;
        private readonly IPortfolioDomainService _domainService;

        public DepositFundsCommand(Guid userId, decimal amount, IPortfolioDomainService domainService)
        {
            _userId = userId;
            _amount = amount;
            _domainService = domainService;
        }

        public async Task ExecuteAsync()
        {
            await _domainService.DepositFundsAsync(_userId, _amount);
        }
    }
}
