using TransactionsService.Data.Entities;
using TransactionsService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;
using Microsoft.Extensions.Logging;

namespace PaymentService.Logic.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(
            ITransactionRepository paymentRepository, 
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

            
        }
    }
}
