using PaymentService.Data.Entities;
using PaymentService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;

namespace PaymentService.Logic.EventHandlers
{
    public class OrderPlacedEventHandler
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEventBus _eventBus;

        public OrderPlacedEventHandler(IPaymentRepository paymentRepository, IEventBus eventBus)
        {
            _paymentRepository = paymentRepository;
            _eventBus = eventBus;
        }

        public async Task HandleOrderPlacedEventAsync(OrderPlacedEvent orderPlacedEvent)
        {
            // Simulate payment processing
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = orderPlacedEvent.OrderId,
                UserId = orderPlacedEvent.UserId,
                Amount = orderPlacedEvent.Price*orderPlacedEvent.Quantity,
                Status = "Approved", // Mock payment status
                ApprovedAt = DateTime.UtcNow
            };

            // Add the payment to the database
            await _paymentRepository.AddAsync(payment);

            // Publish PaymentApprovedEvent
            var paymentApprovedEvent = new PaymentApprovedEvent
            {
                EventId = payment.Id,
                OrderId = payment.OrderId,
                UserId = payment.UserId,
                Amount = payment.Amount,
                CreatedAt = DateTime.UtcNow
            };

            _eventBus.Publish(paymentApprovedEvent, "PaymentExchange"); // Publish to the appropriate exchange
        }
    }
}
