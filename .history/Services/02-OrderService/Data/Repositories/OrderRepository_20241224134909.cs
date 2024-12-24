using OrderService.Data.Entities;
using Shared.Persistence;

namespace OrderService.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context) : base(context) { }
    }
}
