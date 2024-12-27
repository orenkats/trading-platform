using Shared.Persistence;
using TransactionsService.Data.Entities;

namespace TransactionsService.Data.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        // Additional methods specific to Transaction can be added here if needed
    }
}
