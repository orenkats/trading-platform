using PortfolioService.Application.Commands;
using PortfolioService.Application.Queries;
using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;
using PortfolioService.Infrastructure.Persistence.Repositories;

namespace PortfolioService.Application.Services
{
    public class PortfolioAppService : IPortfolioAppService
    {
        private readonly IPortfolioRepository _repository;
        private readonly IPortfolioDomainService _domainService;

        public PortfolioAppService(IPortfolioRepository repository, IPortfolioDomainService domainService)
        {
            _repository = repository;
            _domainService = domainService;
        }

        public async Task DepositFundsAsync(Guid userId, decimal amount)
        {
            var command = new DepositFundsCommand(userId, amount, _repository, _domainService);
            await command.ExecuteAsync();
        }

        public async Task WithdrawFundsAsync(Guid userId, decimal amount)
        {
            var command = new WithdrawFundsCommand(userId, amount, _repository, _domainService);
            await command.ExecuteAsync();
        }

        public async Task AddOrUpdateHoldingAsync(Guid userId, Holding newHolding)
        {
            var command = new AddHoldingCommand(userId, newHolding, _domainService);
            await command.ExecuteAsync();
        }

        public async Task<Portfolio?> GetPortfolioAsync(Guid userId)
        {
            var query = new GetPortfolioQuery(userId, _repository);
            return await query.ExecuteAsync();
        }
    }
}
