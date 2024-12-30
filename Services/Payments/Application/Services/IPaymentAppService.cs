
using PaymentService.Application.DTOs;
public interface IPaymentAppService
{
    Task ProcessDepositAsync(Guid userId, decimal amount);
    Task ProcessWithdrawalAsync(Guid userId, decimal amount);
}
