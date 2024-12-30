
using PaymentService.Application.DTOs;
public interface IPaymentAppService
{
    Task<PaymentResponseDto> ProcessPaymentAsync(Guid userId, decimal amount);
    Task<PaymentResponseDto> ProcessWithdrawalAsync(Guid userId, decimal amount);
}
