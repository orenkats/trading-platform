using PaymentService.Application.Commands;
using PaymentService.Domain.Interfaces;
using Shared.Events;
using Shared.Messaging;

namespace PaymentService.Application.Services
{
    public class PaymentAppService : IPaymentAppService
    {
        private readonly IPaymentDomainService _domainService;
        private readonly IEventBus _eventBus;

        public PaymentAppService(IPaymentDomainService domainService, IEventBus eventBus)
        {
            _domainService = domainService;
            _eventBus = eventBus;
        }

        public async Task ProcessDepositAsync(Guid userId, decimal amount)
        {
            var command = new ProcessDepositCommand(userId, amount, _domainService, _eventBus);
            await command.ExecuteAsync();
        }

        public async Task ProcessWithdrawalAsync(Guid userId, decimal amount)
        {
            var command = new ProcessWithdrawalCommand(userId, amount, _domainService, _eventBus);
            await command.ExecuteAsync();
        }
    }
}
