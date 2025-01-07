using PortfolioService.Application.Commands;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class WithdrawalProcessedEventHandler : IEventHandler<WithdrawalProcessedEvent>
    {
        private readonly WithdrawFundsCommand _withdrawFundsCommand;
        private readonly ILogger<WithdrawalProcessedEventHandler> _logger;

        public WithdrawalProcessedEventHandler(
            WithdrawFundsCommand withdrawFundsCommand,
            ILogger<WithdrawalProcessedEventHandler> logger)
        {
            _withdrawFundsCommand = withdrawFundsCommand;
            _logger = logger;
        }

        public async Task HandleAsync(WithdrawalProcessedEvent withdrawalEvent)
        {
            _logger.LogInformation("Handling WithdrawalProcessedEvent for UserId: {UserId}", withdrawalEvent.UserId);

            if (withdrawalEvent.Status == "Approved")
            {
                await _withdrawFundsCommand.ExecuteAsync(withdrawalEvent.UserId, withdrawalEvent.Amount);
                _logger.LogInformation("Withdrawal processed successfully for UserId: {UserId}", withdrawalEvent.UserId);
            }
            else
            {
                _logger.LogWarning("Withdrawal failed for UserId: {UserId}, Status: {Status}", withdrawalEvent.UserId, withdrawalEvent.Status);
            }
        }
    }
}
