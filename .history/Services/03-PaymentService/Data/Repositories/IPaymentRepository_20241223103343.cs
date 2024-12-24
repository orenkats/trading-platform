using Shared.Persistence;
using PaymentService.Data.Entities;

namespace PaymentService.Data.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        // Additional methods specific to Payment can be added here if needed
    }
}
