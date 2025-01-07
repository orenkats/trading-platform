using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class AddHoldingCommand
    {
        private readonly IPortfolioDomainService _domainService;

        public AddHoldingCommand(IPortfolioDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task ExecuteAsync(Guid _userId,Holding _newHolding)
        {
            // Delegate the entire operation to the domain service
            await _domainService.AddOrUpdateHoldingAsync(_userId, _newHolding);
        }
    }
}
