using System;
using System.Threading.Tasks;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Commands
{
    public class PlaceOrderCommand
    {
        private readonly IOrderDomainService _orderDomainService;

        public PlaceOrderCommand(IOrderDomainService orderDomainService)
        {
            _orderDomainService = orderDomainService;
        }

        public async Task ExecuteAsync(Guid userId, string stockSymbol, int quantity, decimal price)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StockSymbol = stockSymbol,
                Quantity = quantity,
                Price = price,
                CreatedAt = DateTime.UtcNow
            };

            // Delegate the operation to the DomainService
            await _orderDomainService.PlaceOrderAsync(order);
        }
    }
}
