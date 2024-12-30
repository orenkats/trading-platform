using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class WithdrawFundsCommand
    {
        private readonly Guid _userId;
        private readonly decimal _amount;
        private readonly IPortfolioDomainService _domainService;

        public WithdrawFundsCommand(Guid userId, decimal amount, IPortfolioDomainService domainService)
        {
            _userId = userId;
            _amount = amount;
            _domainService = domainService;
        }

        public async Task ExecuteAsync()
        {
            await _domainService.WithdrawFundsAsync(_userId, _amount);
        }
    }
}
