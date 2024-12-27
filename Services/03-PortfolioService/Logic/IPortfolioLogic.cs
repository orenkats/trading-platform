using PortfolioService.Data.Entities;

namespace PortfolioService.Logic
{
    public interface IPortfolioLogic
    {
        Task CreatePortfolioAsync(Portfolio portfolio); // Use the existing method
        Task UpdatePortfolioAsync(Portfolio portfolio);
        Task<Portfolio?> GetPortfolioByUserIdAsync(Guid userId);

        // Holding-specific methods
        Task AddOrUpdateHoldingAsync(Guid userId, Holding holding);

        // Deposit/Withdrawal Methods
        Task<bool> DepositFundsAsync(Guid userId, decimal amount);
        Task<bool> WithdrawFundsAsync(Guid userId, decimal amount);
    }
}
