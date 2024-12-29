using PortfolioService.Domain.Entities;

namespace PortfolioService.Application.Services
{
    public interface IPortfolioAppService
    {
        Task DepositFundsAsync(Guid userId, decimal amount);
        Task WithdrawFundsAsync(Guid userId, decimal amount);
        Task AddOrUpdateHoldingAsync(Guid userId, Holding newHolding);
        Task<Portfolio?> GetPortfolioAsync(Guid userId);
    }
}
