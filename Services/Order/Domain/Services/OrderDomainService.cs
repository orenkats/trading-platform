using System.Net.Http;
using System.Text.Json;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories;
using Shared.Messaging;
using Shared.Events;
using OrderService.Domain.Interfaces;

namespace OrderService.Domain.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEventBus _eventBus;

        public OrderDomainService(IOrderRepository orderRepository, IHttpClientFactory httpClientFactory, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _httpClientFactory = httpClientFactory;
            _eventBus = eventBus;
        }

        public async Task PlaceOrderAsync(Order order)
        {
            // Validate stock symbol
            var isValidStock = await ValidateStockSymbolAsync(order.StockSymbol);
            if (!isValidStock)
            {
                throw new Exception($"Invalid stock symbol: {order.StockSymbol}");
            }

            // Validate account balance
            var hasSufficientBalance = await ValidateAccountBalanceAsync(order.UserId, order.Quantity * order.Price);
            if (!hasSufficientBalance)
            {
                throw new Exception("Insufficient account balance to place the order.");
            }

            // Add the order to the Domainbase
            await _orderRepository.AddAsync(order);

            
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            // Retrieve orders by userId from the Domainbase
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

        private async Task<bool> ValidateAccountBalanceAsync(Guid userId, decimal orderCost)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5003/api/portfolio/balance?userId={userId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch account balance.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var accountBalance = JsonSerializer.Deserialize<decimal>(content);

            return accountBalance >= orderCost;
        }
    }
}
