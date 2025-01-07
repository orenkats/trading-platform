using PortfolioService.Application.Commands;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly AddHoldingCommand _addHoldingCommand;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(
            AddHoldingCommand addHoldingCommand,
            ILogger<OrderPlacedEventHandler> logger)
        {
            _addHoldingCommand = addHoldingCommand;
            _logger = logger;
        }

        public async Task HandleAsync(OrderPlacedEvent orderPlacedEvent)
        {
            _logger.LogInformation("Handling OrderPlacedEvent: {OrderId} for User: {UserId}",
                orderPlacedEvent.OrderId, orderPlacedEvent.UserId);

            try
            {
                // Use the command to handle the operation, passing raw event data
                await _addHoldingCommand.ExecuteAsync(
                    orderPlacedEvent.UserId,
                    orderPlacedEvent.StockSymbol,
                    orderPlacedEvent.Quantity,
                    orderPlacedEvent.Price
                );

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
