using PortfolioService.Data.Entities;
using PortfolioService.Data.Repositories;

namespace PortfolioService.Logic
{
    public class PortfolioLogic : IPortfolioLogic
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioLogic(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<Portfolio> CreatePortfolioForUserAsync(Guid userId)
        {
            var portfolio = new Portfolio
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            await _portfolioRepository.AddAsync(portfolio);
            return portfolio;
        }
    }
}
