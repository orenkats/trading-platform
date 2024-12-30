using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Interfaces;
public interface IPaymentDomainService
{
    Task<Payment> ProcessDepositAsync(Guid userId, decimal amount);
    Task<Payment> ProcessWithdrawalAsync(Guid userId, decimal amount); // Add this line
}
