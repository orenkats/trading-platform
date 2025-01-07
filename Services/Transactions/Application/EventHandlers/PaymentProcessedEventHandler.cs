using Shared.Events;
using Shared.Messaging;
using TransactionService.Application.Commands;
using TransactionService.Domain.Interfaces;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.EventHandlers
{
    public class PaymentProcessedEventHandler : IEventHandler<PaymentProcessedEvent>
    {
        private readonly ITransactionDomainService _domainService;

        public PaymentProcessedEventHandler(ITransactionDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task HandleAsync(PaymentProcessedEvent paymentProcessedEvent)
        {
            if (paymentProcessedEvent.Status == "Approved")
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    UserId = paymentProcessedEvent.UserId,
                    Amount = paymentProcessedEvent.Amount,
                    //Type = paymentProcessedEvent.Type, // Deposit or Withdrawal
                    Status = "Completed",
                    Timestamp = DateTime.UtcNow
                };

                var command = new RecordTransactionCommand(_domainService, transaction);
                await command.ExecuteAsync();
            }
            else
            {
                // Log rejected transaction or take other necessary actions
            }
        }
    }
}
