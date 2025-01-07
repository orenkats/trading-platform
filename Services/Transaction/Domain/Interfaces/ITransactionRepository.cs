using TransactionService.Domain.Entities;

namespace TransactionService.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task AddAsync(Transaction entity);
        Task UpdateAsync(Transaction entity);
        Task DeleteAsync(Guid id);
        // Add additional methods specific to Transaction if needed
    }
}
