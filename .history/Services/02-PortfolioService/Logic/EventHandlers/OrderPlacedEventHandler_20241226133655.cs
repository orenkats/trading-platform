using PortfolioService.Data.Entities;
using PortfolioService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Logic.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(
            IPortfolioRepository portfolioRepository,
            ILogger<OrderPlacedEventHandler> logger)
        {
            _portfolioRepository = portfolioRepository;
            _logger = logger;
        }

        public async Task HandleAsync(OrderPlacedEvent orderPlacedEvent)
        {
            _logger.LogInformation($"Handling OrderPlacedEvent. OrderId: {orderPlacedEvent.OrderId}, UserId: {orderPlacedEvent.UserId}");

            try
            {
                // Fetch the user's portfolio
                var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(orderPlacedEvent.UserId);
                if (portfolio == null)
                {
                    _logger.LogWarning($"No portfolio found for UserId: {orderPlacedEvent.UserId}. Creating a new portfolio.");

                    // Create a new portfolio if one doesn't exist
                    portfolio = new Portfolio
                    {
                        Id = Guid.NewGuid(),
                        UserId = orderPlacedEvent.UserId,
                        CreatedAt = DateTime.UtcNow,
                        AccountBalance = 0 // Default account balance
                    };

                    await _portfolioRepository.AddPortfolioAsync(portfolio);
                }

                // Update holdings or add new holding
                var existingHolding = portfolio.Holdings.FirstOrDefault(h => h.StockSymbol == orderPlacedEvent.StockSymbol);
                if (existingHolding != null)
                {
                    // Update existing holding
                    var totalCost = existingHolding.Quantity * existingHolding.AveragePrice + orderPlacedEvent.Quantity * orderPlacedEvent.Price;
                    existingHolding.Quantity += orderPlacedEvent.Quantity;
                    existingHolding.AveragePrice = totalCost / existingHolding.Quantity;
                }
                else
                {
                    // Add new holding
                    portfolio.Holdings.Add(new Holding
                    {
                        PortfolioId = portfolio.Id,
                        StockSymbol = orderPlacedEvent.StockSymbol,
                        Quantity = orderPlacedEvent.Quantity,
                        AveragePrice = orderPlacedEvent.Price
                    });
                }

                await _portfolioRepository.UpdatePortfolioAsync(portfolio);
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
