using PortfolioService.Domain.Entities;

namespace PortfolioService.Domain.Interfaces
{
    public interface IPortfolioDomainService
    {
        Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId);
        Task AddOrUpdateHoldingAsync(Guid userId, Holding newHolding);
        Task DepositFundsAsync(Guid userId, decimal amount);
        Task WithdrawFundsAsync(Guid userId, decimal amount);
        decimal CalculateUpdatedBalance(decimal currentBalance, decimal amount, bool isDeposit);
    }
}
