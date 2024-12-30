using PaymentService.Application.DTOs;
using System;
using System.Threading.Tasks;

public interface IPaymentAppService
{
    Task<PaymentResponseDto> ProcessPaymentAsync(Guid userId, decimal amount);
}

