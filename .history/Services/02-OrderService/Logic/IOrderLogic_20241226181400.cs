using OrderService.Data.Entities;

namespace OrderService.Logic
{
    public interface IOrderLogic
    {
        Task PlaceOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        
    }
}
