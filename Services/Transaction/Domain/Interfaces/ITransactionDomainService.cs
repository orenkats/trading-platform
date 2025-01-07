using TransactionService.Domain.Entities;

namespace TransactionService.Domain.Interfaces
{
    public interface ITransactionDomainService
    {
        Task RecordTransactionAsync(Transaction transaction);
    }
}
