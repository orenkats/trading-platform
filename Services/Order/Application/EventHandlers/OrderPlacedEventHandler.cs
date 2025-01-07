using Shared.Events;
using Shared.Messaging;
using OrderService.Application.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Application.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly PlaceOrderCommand _command;

        public OrderPlacedEventHandler(PlaceOrderCommand command)
        {
            _command = command;
        }

        public async Task HandleAsync(OrderPlacedEvent orderPlacedEvent)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(), // Generate a new unique ID for the order
                UserId = orderPlacedEvent.UserId,
                StockSymbol = orderPlacedEvent.StockSymbol,
                Quantity = orderPlacedEvent.Quantity,
                Price = orderPlacedEvent.Price,
                CreatedAt = DateTime.UtcNow
            };

            // Delegate the responsibility to the PlaceOrderCommand
            await _command.ExecuteAsync(order.UserId, order.StockSymbol, order.Quantity, order.Price);
        }
    }
}
