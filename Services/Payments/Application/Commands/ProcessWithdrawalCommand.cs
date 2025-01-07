using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using Shared.Events;
using Shared.Messaging;

namespace PaymentService.Application.Commands
{
    public class ProcessWithdrawalCommand
    {
        private readonly Guid _userId;
        private readonly decimal _amount;
        private readonly IPaymentDomainService _domainService;
        private readonly IEventBus _eventBus;

        public ProcessWithdrawalCommand(Guid userId, decimal amount, IPaymentDomainService domainService, IEventBus eventBus)
        {
            _userId = userId;
            _amount = amount;
            _domainService = domainService;
            _eventBus = eventBus;
        }

        public async Task ExecuteAsync()
        {
            // Call the domain logic to process the withdrawal
            var payment = await _domainService.ProcessWithdrawalAsync(_userId, _amount);

            // Publish the event based on the payment outcome
            var withdrawalProcessedEvent = new WithdrawalProcessedEvent
            {
                EventId = Guid.NewGuid(),
                UserId = _userId,
                PaymentId = payment.Id,
                Amount = payment.Amount,
                Status = payment.Status,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(withdrawalProcessedEvent, "PaymentExchange");
        }
    }
}
