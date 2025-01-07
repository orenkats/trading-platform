using TransactionService.Domain.Entities;
using TransactionService.Domain.Interfaces;
using TransactionService.Infrastructure.Repositories;

namespace TransactionService.Domain.Services
{
    public class TransactionDomainService : ITransactionDomainService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionDomainService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task RecordTransactionAsync(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
        }
    }
}
