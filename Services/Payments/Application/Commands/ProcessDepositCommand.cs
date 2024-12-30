using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Entities;
using Shared.Events;
using Shared.Messaging;

namespace PaymentService.Application.Commands
{
    public class ProcessDepositCommand
    {
        private readonly Guid _userId;
        private readonly decimal _amount;
        private readonly IPaymentDomainService _domainService;
        private readonly IEventBus _eventBus;

        public ProcessDepositCommand(Guid userId, decimal amount, IPaymentDomainService domainService, IEventBus eventBus)
        {
            _userId = userId;
            _amount = amount;
            _domainService = domainService;
            _eventBus = eventBus;
        }

        public async Task<Payment> ExecuteAsync()
        {
            // Process the deposit
            var payment = await _domainService.ProcessDepositAsync(_userId, _amount);

            // Publish PaymentProcessedEvent
            var paymentEvent = new PaymentProcessedEvent
            {
                UserId = _userId,
                PaymentId = payment.Id,
                Amount = _amount,
                Status = payment.Status,
                Timestamp = DateTime.UtcNow
            };

            _eventBus.Publish(paymentEvent, "PaymentExchange");

            return payment;
        }
    }
}