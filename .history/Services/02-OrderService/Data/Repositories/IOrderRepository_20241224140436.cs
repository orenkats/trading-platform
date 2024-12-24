using OrderService.Data.Entities;
using Shared.Persistence;

namespace OrderService.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}
