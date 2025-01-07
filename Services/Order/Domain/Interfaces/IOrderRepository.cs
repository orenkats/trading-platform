using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}
