using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using PaymentService.Infrastructure.Persistence.Repositories;

public class PaymentDomainService : IPaymentDomainService
{
    private readonly IPaymentRepository _repository;

    public PaymentDomainService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Payment> ProcessPaymentAsync(Guid userId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.");
        }

        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = amount,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(payment);

        // Simulate payment processing
        payment.Status = "Completed";
        await _repository.UpdateAsync(payment);

        return payment;
    }

    public async Task<Payment> ProcessWithdrawalAsync(Guid userId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.");
        }

        var withdrawal = new Payment
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = -amount, // Negative amount for withdrawals
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(withdrawal);

        // Simulate withdrawal processing
        withdrawal.Status = "Completed";
        await _repository.UpdateAsync(withdrawal);

        return withdrawal;
    }
}
