using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using PaymentService.Application.DTOs;
public class PaymentAppService : IPaymentAppService
{
    private readonly IPaymentDomainService _domainService;

    public PaymentAppService(IPaymentDomainService domainService)
    {
        _domainService = domainService;
    }

    public async Task<PaymentResponseDto> ProcessPaymentAsync(Guid userId, decimal amount)
    {
        var payment = await _domainService.ProcessPaymentAsync(userId, amount);

        return new PaymentResponseDto
        {
            PaymentId = payment.Id,
            Status = payment.Status,
            CreatedAt = payment.CreatedAt
        };
    }

    public async Task<PaymentResponseDto> ProcessWithdrawalAsync(Guid userId, decimal amount)
    {
        var withdrawal = await _domainService.ProcessWithdrawalAsync(userId, amount);

        return new PaymentResponseDto
        {
            PaymentId = withdrawal.Id,
            Status = withdrawal.Status,
            CreatedAt = withdrawal.CreatedAt
        };
    }
}
