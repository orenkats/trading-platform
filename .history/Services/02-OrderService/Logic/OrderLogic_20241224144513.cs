using System.Net.Http;
using System.Text.Json;
using OrderService.Data.Entities;
using OrderService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;

namespace OrderService.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEventBus _eventBus;

        public OrderLogic(IOrderRepository orderRepository, IHttpClientFactory httpClientFactory, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _httpClientFactory = httpClientFactory;
            _eventBus = eventBus;
        }

        public async Task PlaceOrderAsync(Order order)
        {
            // Validate the stock symbol
            var isValidStock = await ValidateStockSymbolAsync(order.StockSymbol);
            if (!isValidStock)
            {
                throw new Exception($"Invalid stock symbol: {order.StockSymbol}");
            }

            // Add the order to the database
            await _orderRepository.AddAsync(order);

            // Publish OrderPlacedEvent to RabbitMQ
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
            // Retrieve orders by userId from the database
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        private async Task<bool> ValidateStockSymbolAsync(string stockSymbol)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5002/api/stock/validate/{stockSymbol}");

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(content);
        }
    }
}
