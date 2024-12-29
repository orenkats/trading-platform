using PortfolioService.Domain.Entities;
using PortfolioService.Infrastructure.Persistence.Repositories;
using PortfolioService.Domain.Services;

namespace PortfolioService.Application.Queries
{
    public class GetPortfolioQuery
    {
        private readonly IPortfolioRepository _repository;
        private readonly Guid _userId;

        public GetPortfolioQuery(Guid userId, IPortfolioRepository repository)
        {
            _userId = userId;
            _repository = repository;
        }

        public async Task<Portfolio?> ExecuteAsync()
        {
            return await _repository.GetPortfolioByUserIdAsync(_userId);
        }
    }
}
