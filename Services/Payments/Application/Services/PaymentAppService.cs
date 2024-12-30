using PaymentService.Application.DTOs;
using PaymentService.Application.Commands;
using PaymentService.Domain.Interfaces;


namespace PaymentService.Application.Services
{
    public class PaymentAppService : IPaymentAppService
    {
        private readonly IPaymentDomainService _domainService;

        public PaymentAppService(IPaymentDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(Guid userId, decimal amount)
        {
            var command = new ProcessPaymentCommand(userId, amount, _domainService);
            return await command.ExecuteAsync();
        }
    }
}
