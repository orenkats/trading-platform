using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbContext _context;

        public PaymentRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> GetByIdAsync(Guid id) => await _context.Set<Payment>().FindAsync(id);

        public async Task<IEnumerable<Payment>> GetAllAsync() => await _context.Set<Payment>().ToListAsync();

        public async Task AddAsync(Payment entity)
        {
            await _context.Set<Payment>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment entity)
        {
            _context.Set<Payment>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Payment>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId)
        {
            return await _context.Set<Payment>().Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
