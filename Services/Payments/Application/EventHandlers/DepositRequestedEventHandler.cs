using PaymentService.Application.Services;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PaymentService.Application.EventHandlers
{
    public class DepositRequestedEventHandler : IEventHandler<DepositRequestedEvent>
    {
        private readonly IPaymentAppService _paymentAppService;
        private readonly ILogger<DepositRequestedEventHandler> _logger;

        public DepositRequestedEventHandler(IPaymentAppService paymentAppService, ILogger<DepositRequestedEventHandler> logger)
        {
            _paymentAppService = paymentAppService;
            _logger = logger;
        }

        public async Task HandleAsync(DepositRequestedEvent depositEvent)
        {
            _logger.LogInformation("Handling DepositRequestedEvent for UserId: {UserId}", depositEvent.UserId);

            try
            {
                await _paymentAppService.ProcessDepositAsync(depositEvent.UserId, depositEvent.Amount);
                _logger.LogInformation("Successfully processed deposit for UserId: {UserId}", depositEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling DepositRequestedEvent for UserId: {UserId}", depositEvent.UserId);
                throw;
            }
        }
    }
}
