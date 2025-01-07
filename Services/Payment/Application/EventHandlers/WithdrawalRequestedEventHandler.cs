using PaymentService.Application.Commands;
using Shared.Events;
using Shared.Messaging;

namespace PaymentService.Application.EventHandlers
{
    public class WithdrawalRequestedEventHandler : IEventHandler<WithdrawalRequestedEvent>
    {
        private readonly ProcessWithdrawalCommand _processWithdrawalCommand;

        public WithdrawalRequestedEventHandler(ProcessWithdrawalCommand processWithdrawalCommand)
        {
            _processWithdrawalCommand = processWithdrawalCommand;
        }

        public async Task HandleAsync(WithdrawalRequestedEvent withdrawalEvent)
        {
            // Execute the withdrawal command
            await _processWithdrawalCommand.ExecuteAsync();
        }
    }
}
