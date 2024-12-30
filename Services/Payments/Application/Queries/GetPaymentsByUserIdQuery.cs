using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentService.Application.Queries
{
    public class GetPaymentsByUserIdQuery
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentsByUserIdQuery(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> ExecuteAsync(Guid userId)
        {
            return await _paymentRepository.GetPaymentsByUserIdAsync(userId);
        }
    }
}
