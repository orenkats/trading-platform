using OrderService.Data.Entities;
using Shared.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}
