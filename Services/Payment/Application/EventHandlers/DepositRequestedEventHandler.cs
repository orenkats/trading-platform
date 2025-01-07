using PaymentService.Application.Commands;
using Shared.Events;
using Shared.Messaging;

namespace PaymentService.Application.EventHandlers
{
    public class DepositRequestedEventHandler : IEventHandler<DepositRequestedEvent>
    {
        private readonly ProcessDepositCommand _processDepositCommand;

        public DepositRequestedEventHandler(ProcessDepositCommand processDepositCommand)
        {
            _processDepositCommand = processDepositCommand;
        }

        public async Task HandleAsync(DepositRequestedEvent depositEvent)
        {
            // Execute the deposit command
            await _processDepositCommand.ExecuteAsync();
        }
    }
}
