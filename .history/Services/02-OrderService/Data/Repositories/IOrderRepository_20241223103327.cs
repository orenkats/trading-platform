using Shared.Persistence;
using OrderService.Data.Entities;

namespace OrderService.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        // Additional methods specific to Order can be added here if needed
    }
}
