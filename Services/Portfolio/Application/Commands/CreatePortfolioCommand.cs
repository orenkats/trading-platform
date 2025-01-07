using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class CreatePortfolioCommand
    {
        private readonly IPortfolioDomainService _domainService;

        public CreatePortfolioCommand(IPortfolioDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task ExecuteAsync(Guid _userId)
        {
            // Call domain service to handle the business logic
            await _domainService.CreatePortfolioAsync(_userId);
        }
    }
}
