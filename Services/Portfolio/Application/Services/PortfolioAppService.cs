using PortfolioService.Application.Commands;
using PortfolioService.Application.Queries;
using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Infrastructure.Persistence.Repositories;

namespace PortfolioService.Application.Services
{
    public class PortfolioAppService : IPortfolioAppService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioDomainService _domainService;

        public PortfolioAppService(IPortfolioRepository repository, IPortfolioDomainService domainService)
        {
            _portfolioRepository = repository;
            _domainService = domainService;
        }
        public async Task CreatePortfolioAsync(Guid userId)
        {
            var command = new CreatePortfolioCommand(userId, _domainService);
            await command.ExecuteAsync();
        }

        public async Task DepositFundsAsync(Guid userId, decimal amount)
        {
            var command = new DepositFundsCommand(userId, amount, _domainService);
            await command.ExecuteAsync();
        }

        public async Task WithdrawFundsAsync(Guid userId, decimal amount)
        {
            var command = new WithdrawFundsCommand(userId, amount, _domainService);
            await command.ExecuteAsync();
        }

        public async Task AddOrUpdateHoldingAsync(Guid userId, Holding newHolding)
        {
            var command = new AddHoldingCommand(userId, newHolding, _domainService);
            await command.ExecuteAsync();
        }

        public async Task<Portfolio?> GetPortfolioAsync(Guid userId)
        {
            var query = new GetPortfolioQuery(userId, _portfolioRepository);
            return await query.ExecuteAsync();
        }

        // Add the new method for GetAccountBalanceQuery
        public async Task<decimal> GetAccountBalanceAsync(Guid userId)
        {
            var query = new GetAccountBalanceQuery(_portfolioRepository);
            return await query.ExecuteAsync(userId);
        }
    }
}
