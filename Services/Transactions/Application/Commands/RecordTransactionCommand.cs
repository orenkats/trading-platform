using TransactionService.Domain.Entities;
using TransactionService.Domain.Interfaces;

namespace TransactionService.Application.Commands
{
    public class RecordTransactionCommand
    {
        private readonly ITransactionDomainService _domainService;
        private readonly Transaction _transaction;

        public RecordTransactionCommand(ITransactionDomainService domainService, Transaction transaction)
        {
            _domainService = domainService;
            _transaction = transaction;
        }

        public async Task ExecuteAsync()
        {
            await _domainService.RecordTransactionAsync(_transaction);
        }
    }
}
