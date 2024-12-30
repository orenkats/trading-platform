using PaymentService.Domain.Entities;
using Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId)
        {
            return await _dbSet.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
