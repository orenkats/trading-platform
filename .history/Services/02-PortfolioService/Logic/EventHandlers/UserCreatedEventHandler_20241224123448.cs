using PortfolioService.Data.Entities;
using Shared.Events;

namespace PortfolioService.Logic.EventHandlers
{
    public class UserCreatedEventHandler
    {
        private readonly IPortfolioLogic _portfolioLogic;

        public UserCreatedEventHandler(IPortfolioLogic portfolioLogic)
        {
            _portfolioLogic = portfolioLogic;
        }

        public async Task HandleUserCreatedEventAsync(UserCreatedEvent userCreatedEvent)
        {
            var portfolio = new Portfolio
            {
                UserId = userCreatedEvent.UserId,
                Holdings = new List<Holding>(),
                CreatedAt = DateTime.UtcNow
            };

            // Add the new portfolio for the created user
            await _portfolioLogic.CreatePortfolioAsync(portfolio);
        }
    }
}
