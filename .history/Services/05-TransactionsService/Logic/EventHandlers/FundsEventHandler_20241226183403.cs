using TransactionsService.Data.Entities;
using TransactionsService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;
using Microsoft.Extensions.Logging;

namespace TransactionsService.Logic.EventHandlers
{
    public class FundsEventHandler : IEventHandler<object> // Use a base or object if event type varies
    {
        private readonly ITransactionRepository _transactionRepository;

        public FundsEventHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task HandleAsync(object @event)
        {
            switch (@event)
            {
                case FundsDepositedEvent depositedEvent:
                    var depositTransaction = new Transaction
                    {
                        Id = Guid.NewGuid(),
                        UserId = depositedEvent.UserId,
                        Amount = depositedEvent.Amount,
                        Type = "Deposit",
                        CreatedAt = depositedEvent.Timestamp
                    };
                    await _transactionRepository.AddAsync(depositTransaction);
                    break;

                case FundsWithdrawnEvent withdrawnEvent:
                    var withdrawalTransaction = new Transaction
                    {
                        Id = Guid.NewGuid(),
                        UserId = withdrawnEvent.UserId,
                        Amount = withdrawnEvent.Amount,
                        Type = "Withdrawal",
                        CreatedAt = withdrawnEvent.Timestamp
                    };
                    await _transactionRepository.AddAsync(withdrawalTransaction);
                    break;

                default:
                    throw new InvalidOperationException("Unhandled event type");
            }
        }
    }

}
