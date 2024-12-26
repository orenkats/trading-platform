using PaymentService.Data.Entities;
using PaymentService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;
using Microsoft.Extensions.Logging;

namespace PaymentService.Logic.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(
            IPaymentRepository paymentRepository, 
            IEventBus eventBus,
            ILogger<OrderPlacedEventHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(OrderPlacedEvent orderPlacedEvent)
        {
            _logger.LogInformation($"Handling OrderPlacedEvent. OrderId: {orderPlacedEvent.OrderId}");

            try
            {
                // Simulate payment processing
                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderPlacedEvent.OrderId,
                    UserId = orderPlacedEvent.UserId,
                    Amount = orderPlacedEvent.Price * orderPlacedEvent.Quantity,
                    Status = "Approved", // Mock payment status
                    ApprovedAt = DateTime.UtcNow
                };

                // Add the payment to the database
                await _paymentRepository.AddAsync(payment);
                _logger.LogInformation($"Payment processed successfully. PaymentId: {payment.Id}");

                var paymentApprovedEvent = new PaymentApprovedEvent
                {
                    EventId = payment.Id,
                    OrderId = payment.OrderId,
                    UserId = payment.UserId,
                    Amount = payment.Amount
                };

                _eventBus.Publish(paymentApprovedEvent, "PaymentExchange");
                _logger.LogInformation($"PaymentApprovedEvent published. PaymentId: {payment.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling OrderPlacedEvent. OrderId: {orderPlacedEvent.OrderId}. Exception: {ex.Message}");
                throw;
            }
        }
    }
}
