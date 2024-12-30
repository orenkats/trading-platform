using PaymentService.Domain.Interfaces;
using PaymentService.Application.DTOs;

namespace PaymentService.Application.Commands
{
    public class ProcessPaymentCommand
    {
        private readonly Guid _userId;
        private readonly decimal _amount;
        private readonly IPaymentDomainService _domainService;

        public ProcessPaymentCommand(Guid userId, decimal amount, IPaymentDomainService domainService)
        {
            _userId = userId;
            _amount = amount;
            _domainService = domainService;
        }

        public async Task<PaymentResponseDto> ExecuteAsync()
        {
            var payment = await _domainService.ProcessPaymentAsync(_userId, _amount);

            return new PaymentResponseDto
            {
                PaymentId = payment.Id,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt
            };
        }
    }
}
