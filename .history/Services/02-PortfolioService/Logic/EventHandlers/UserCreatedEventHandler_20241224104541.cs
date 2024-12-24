using PortfolioService.Data.Entities;
using PortfolioService.Data.Repositories;
using Shared.Events;

namespace PortfolioService.Logic.EventHandlers
{
    public class UserCreatedEventHandler
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UserCreatedEventHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task HandleUserCreatedEventAsync(UserCreatedEvent userCreatedEvent)
        {
            var portfolio = new Portfolio
            {
                Id = Guid.NewGuid(),
                UserId = userCreatedEvent.UserId,
                Holdings = new List<Holding>() // Initialize empty holdings
            };

            await _portfolioRepository.AddAsync(portfolio);
        }
    }
}
