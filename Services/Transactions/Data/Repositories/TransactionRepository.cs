using Microsoft.EntityFrameworkCore;
using Shared.Persistence;
using TransactionsService.Data.Entities;

namespace TransactionsService.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(TransactionDbContext context) : base(context)
        {
            
        }

        // Additional methods for the Transaction repository can be added here if needed
        // For example, you could add methods to get transactions by userId, type, or status.
    }
}
