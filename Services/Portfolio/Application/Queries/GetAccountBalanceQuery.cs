using PortfolioService.Infrastructure.Persistence.Repositories;
using PortfolioService.Domain.Exceptions;

namespace PortfolioService.Application.Queries
{
    public class GetAccountBalanceQuery
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public GetAccountBalanceQuery(IPortfolioRepository repository)
        {
            _portfolioRepository = repository;
        }

        public async Task<decimal> ExecuteAsync(Guid userId)
        {
            // Fetch the portfolio from the repository
            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId)
                ?? throw new PortfolioNotFoundException(userId);

            // Return the account balance
            return portfolio.AccountBalance;
        }
    }
}
