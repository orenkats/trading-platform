using PaymentService.Data.Entities;
using Shared.Persistence;

namespace PaymentService.Data.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment?> GetByOrderIdAsync(Guid orderId);
    }
}
