using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Interfaces;

public interface IPaymentDomainService
{
    Task<Payment> ProcessPaymentAsync(Guid userId, decimal amount);
}
