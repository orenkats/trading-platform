using TransactionsService.Data.Entities;
using TransactionsService.Data.Repositories;
using Shared.Messaging;
using Shared.Events;
using Microsoft.Extensions.Logging;

namespace TransactionsService.Logic.EventHandlers
{
    public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(
            ITransactionRepository transactionRepository, 
            IEventBus eventBus,
            ILogger<OrderPlacedEventHandler> logger)
        {
            _transactionRepository = transactionRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(OrderPlacedEvent orderPlacedEvent)
        {
            _logger.LogInformation($"Handling OrderPlacedEvent. OrderId: {orderPlacedEvent.OrderId}");

            
        }
    }
}
