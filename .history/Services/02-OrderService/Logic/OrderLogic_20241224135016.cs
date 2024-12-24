using OrderService.Data.Entities;
using OrderService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;

namespace OrderService.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        public OrderLogic(IOrderRepository orderRepository, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _eventBus = eventBus;
        }

        public async Task PlaceOrderAsync(Order order)
        {
            // Validate StockSymbol (ensure it exists in StockCatalog)
            // This logic will require integration with the StockCatalogService.

            // Save the order
            await _orderRepository.AddAsync(order);

            // Publish OrderPlacedEvent
            var orderPlacedEvent = new OrderPlacedEvent
            {
                OrderId = order.Id,
                UserId = order.UserId,
                StockSymbol = order.StockSymbol,
                Quantity = order.Quantity,
                Price = order.Price,
                CreatedAt = order.CreatedAt
            };

            _eventBus.Publish(orderPlacedEvent, "OrderExchange");
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _orderRepository.GetAllAsync(o => o.UserId == userId);
        }
    }
}
