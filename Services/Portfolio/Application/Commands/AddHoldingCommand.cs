using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class AddHoldingCommand
    {
        private readonly Guid _userId;
        private readonly Holding _newHolding;
        private readonly IPortfolioDomainService _domainService;

        public AddHoldingCommand(Guid userId, Holding newHolding, IPortfolioDomainService domainService)
        {
            _userId = userId;
            _newHolding = newHolding;
            _domainService = domainService;
        }

        public async Task ExecuteAsync()
        {
            // Delegate the entire operation to the domain service
            await _domainService.AddOrUpdateHoldingAsync(_userId, _newHolding);
        }
    }
}
