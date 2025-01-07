using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using PaymentService.Infrastructure.Repositories;

namespace PaymentService.Domain.Services
{
    public class PaymentDomainService : IPaymentDomainService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentDomainService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> ProcessDepositAsync(Guid userId, decimal amount)
        {
            // Simulate payment processing with a bank or payment gateway
            var paymentStatus = SimulatePaymentProcessing();

            // Create and save the payment record
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Amount = amount,
                Status = paymentStatus ? "Approved" : "Rejected",
                Timestamp = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);

            // Return the processed payment
            return payment;
        }

        public async Task<Payment> ProcessWithdrawalAsync(Guid userId, decimal amount)
        {
            // Simulate payment processing with a bank or payment gateway
            var paymentStatus = SimulatePaymentProcessing();

            // Create and save the payment record
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Amount = amount,
                Status = paymentStatus ? "Approved" : "Rejected",
                Timestamp = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);

            // Return the processed payment
            return payment;
        }

        private bool SimulatePaymentProcessing()
        {
            // Simulate external bank or payment gateway logic
            var random = new Random();
            return random.Next(0, 2) == 1;
        }
    }
}
