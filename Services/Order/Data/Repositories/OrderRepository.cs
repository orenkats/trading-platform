using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;
using Shared.Persistence;

namespace OrderService.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrderDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _context.Set<Order>().Where(o => o.UserId == userId).ToListAsync();
    }
}

}
