using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderDomainService
    {
        Task PlaceOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        
    }
}
