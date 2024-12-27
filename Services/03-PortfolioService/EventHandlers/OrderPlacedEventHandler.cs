using PortfolioService.Data.Entities;
using PortfolioService.Logic;
using Shared.Messaging;
using Shared.Events;
using Microsoft.Extensions.Logging;

namespace PortfolioService.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly IPortfolioLogic _portfolioLogic;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(
            IPortfolioLogic portfolioLogic,
            ILogger<OrderPlacedEventHandler> logger)
        {
            _portfolioLogic = portfolioLogic;
            _logger = logger;
        }

        public async Task HandleAsync(OrderPlacedEvent orderPlacedEvent)
        {
            _logger.LogInformation($"Handling OrderPlacedEvent. OrderId: {orderPlacedEvent.OrderId}, UserId: {orderPlacedEvent.UserId}");

            try
            {
                // Fetch the user's existing portfolio (no need to create it)
                var portfolio = await _portfolioLogic.GetPortfolioByUserIdAsync(orderPlacedEvent.UserId);
                if (portfolio == null)
                {
                    _logger.LogError($"No portfolio found for UserId: {orderPlacedEvent.UserId}. This should not happen.");
                    throw new InvalidOperationException($"Portfolio not found for UserId: {orderPlacedEvent.UserId}");
                }

                // Use the AddOrUpdateHoldingAsync method to add or update the holding
                var holding = new Holding
                {
                    PortfolioId = portfolio.Id,
                    StockSymbol = orderPlacedEvent.StockSymbol,
                    Quantity = orderPlacedEvent.Quantity,
                    AveragePrice = orderPlacedEvent.Price
                };

                await _portfolioLogic.AddOrUpdateHoldingAsync(orderPlacedEvent.UserId, holding);

                _logger.LogInformation($"Portfolio updated successfully for UserId: {orderPlacedEvent.UserId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling OrderPlacedEvent. OrderId: {orderPlacedEvent.OrderId}. Exception: {ex.Message}");
                throw;
            }
        }
    }
}
