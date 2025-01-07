using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Payment>> GetAllAsync();
        Task AddAsync(Payment entity);
        Task UpdateAsync(Payment entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId);
    }
}
