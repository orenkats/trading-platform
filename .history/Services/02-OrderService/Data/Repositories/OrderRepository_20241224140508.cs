using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;
using Shared.Persistence;

namespace OrderService.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DbContext _context;

        public OrderRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Set<Order>().Where(o => o.UserId == userId).ToListAsync();
        }
    }
}
