using PortfolioService.Application.Commands;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class DepositProcessedEventHandler : IEventHandler<DepositProcessedEvent>
    {
        private readonly DepositFundsCommand _depositFundsCommand;
        private readonly ILogger<DepositProcessedEventHandler> _logger;

        public DepositProcessedEventHandler(
            DepositFundsCommand depositFundsCommand,
            ILogger<DepositProcessedEventHandler> logger)
        {
            _depositFundsCommand = depositFundsCommand;
            _logger = logger;
        }

        public async Task HandleAsync(DepositProcessedEvent depositEvent)
        {
            _logger.LogInformation("Handling DepositProcessedEvent for UserId: {UserId}", depositEvent.UserId);

            if (depositEvent.Status == "Approved")
            {
                await _depositFundsCommand.ExecuteAsync(depositEvent.UserId, depositEvent.Amount);
                _logger.LogInformation("Deposit processed successfully for UserId: {UserId}", depositEvent.UserId);
            }
            else
            {
                _logger.LogWarning("Deposit failed for UserId: {UserId}, Status: {Status}", depositEvent.UserId, depositEvent.Status);
            }
        }
    }
}
