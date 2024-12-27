using Microsoft.EntityFrameworkCore;
using PaymentService.Data.Entities;
using Shared.Persistence;

namespace PaymentService.Data.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }
    }
}
