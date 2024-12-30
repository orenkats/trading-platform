using PortfolioService.Domain.Entities;
using PortfolioService.Infrastructure.Persistence.Repositories;
using PortfolioService.Domain.Services;

namespace PortfolioService.Application.Queries
{
    public class GetPortfolioQuery
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly Guid _userId;

        public GetPortfolioQuery(Guid userId, IPortfolioRepository repository)
        {
            _userId = userId;
            _portfolioRepository = repository;
        }

        public async Task<Portfolio?> ExecuteAsync()
        {
            return await _portfolioRepository.GetPortfolioByUserIdAsync(_userId);
        }
    }
}
