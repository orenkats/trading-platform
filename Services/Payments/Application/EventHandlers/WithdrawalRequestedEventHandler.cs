using PaymentService.Application.Services;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PaymentService.Application.EventHandlers
{
    public class WithdrawalRequestedEventHandler : IEventHandler<WithdrawalRequestedEvent>
    {
        private readonly IPaymentAppService _paymentAppService;
        private readonly ILogger<WithdrawalRequestedEventHandler> _logger;

        public WithdrawalRequestedEventHandler(IPaymentAppService paymentAppService, ILogger<WithdrawalRequestedEventHandler> logger)
        {
            _paymentAppService = paymentAppService;
            _logger = logger;
        }

        public async Task HandleAsync(WithdrawalRequestedEvent withdrawalEvent)
        {
            _logger.LogInformation("Handling WithdrawalRequestedEvent for UserId: {UserId}", withdrawalEvent.UserId);

            try
            {
                await _paymentAppService.ProcessWithdrawalAsync(withdrawalEvent.UserId, withdrawalEvent.Amount);
                _logger.LogInformation("Successfully processed withdrawal for UserId: {UserId}", withdrawalEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling WithdrawalRequestedEvent for UserId: {UserId}", withdrawalEvent.UserId);
                throw;
            }
        }
    }
}
