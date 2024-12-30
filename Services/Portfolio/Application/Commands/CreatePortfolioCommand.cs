using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class CreatePortfolioCommand
    {
        private readonly Guid _userId;
        private readonly IPortfolioDomainService _domainService;

        public CreatePortfolioCommand(Guid userId, IPortfolioDomainService domainService)
        {
            _userId = userId;
            _domainService = domainService;
        }

        public async Task ExecuteAsync()
        {
            // Call domain service to handle the business logic
            await _domainService.CreatePortfolioAsync(_userId);
        }
    }
}
