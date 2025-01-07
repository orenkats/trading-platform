using PortfolioService.Domain.Entities;
using PortfolioService.Domain.Interfaces;

namespace PortfolioService.Application.Commands
{
    public class AddHoldingCommand
    {
        private readonly IPortfolioDomainService _domainService;

        public AddHoldingCommand(IPortfolioDomainService domainService)
        {
            _domainService = domainService;
        }

        public async Task ExecuteAsync(Guid userId, string stockSymbol, int quantity, decimal price)
        {
            var newHolding = new Holding
            {
                StockSymbol = stockSymbol,
                Quantity = quantity,
                AveragePrice = price
            };

            // Delegate the entire operation to the domain service
            await _domainService.AddOrUpdateHoldingAsync(userId, newHolding);
        }
    }
}
