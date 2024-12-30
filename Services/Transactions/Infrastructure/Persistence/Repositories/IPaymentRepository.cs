using PaymentService.Domain.Entities;
using Shared.Persistence;

namespace PaymentService.Infrastructure.Persistence.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId);
    }
}
