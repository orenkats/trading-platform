using Shared.Persistence;
using TransactionService.Domain.Entities;

namespace TransactionService.Infrastructure.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        // Additional methods specific to Transaction can be added here if needed
    }
}
