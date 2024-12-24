using PaymentService.Data.Entities;
using PaymentService.Data.Repositories;
using Shared.Events;

namespace PaymentService.Logic.EventHandlers
{
    public class OrderPlacedEventHandler
    {
        private readonly IPaymentRepository _paymentRepository;

        public OrderPlacedEventHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task HandleOrderPlacedEventAsync(OrderPlacedEvent orderPlacedEvent)
        {
            var payment = new Payment
            {
                OrderId = orderPlacedEvent.OrderId,
                Amount = orderPlacedEvent.Price * orderPlacedEvent.Quantity
            };

            await _paymentRepository.AddAsync(payment);
        }
    }
}
