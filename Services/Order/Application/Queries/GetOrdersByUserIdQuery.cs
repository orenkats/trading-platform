using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Queries
{
    public class GetOrdersByUserIdQuery
    {
        private readonly IOrderDomainService _orderDomainService;

        public GetOrdersByUserIdQuery(IOrderDomainService orderDomainService)
        {
            _orderDomainService = orderDomainService;
        }

        public async Task<IEnumerable<Order>> ExecuteAsync(Guid userId)
        {
            // Delegate the operation to the DomainService
            return await _orderDomainService.GetOrdersByUserIdAsync(userId);
        }
    }
}
