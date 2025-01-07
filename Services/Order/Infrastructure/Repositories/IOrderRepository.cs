using OrderService.Domain.Entities;
using Shared.Persistence;

namespace OrderService.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}
