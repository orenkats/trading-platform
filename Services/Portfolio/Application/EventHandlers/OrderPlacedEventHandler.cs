using PortfolioService.Application.Services;
using PortfolioService.Domain.Entities;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly IPortfolioAppService _appService;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(
            IPortfolioAppService appService,
            ILogger<OrderPlacedEventHandler> logger)
        {
            _appService = appService;
            _logger = logger;
        }

        public async Task HandleAsync(OrderPlacedEvent orderPlacedEvent)
        {
            _logger.LogInformation("Handling OrderPlacedEvent: {OrderId} for User: {UserId}",
                orderPlacedEvent.OrderId, orderPlacedEvent.UserId);

            try
            {
                var newHolding = new Holding
                {
                    StockSymbol = orderPlacedEvent.StockSymbol,
                    Quantity = orderPlacedEvent.Quantity,
                    AveragePrice = orderPlacedEvent.Price
                };

                // Delegate to application service
                await _appService.AddOrUpdateHoldingAsync(orderPlacedEvent.UserId, newHolding);

                _logger.LogInformation("Successfully handled OrderPlacedEvent for User: {UserId}", orderPlacedEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to handle OrderPlacedEvent: {OrderId}", orderPlacedEvent.OrderId);
                throw;
            }
        }
    }
}
