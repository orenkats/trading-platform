using PortfolioService.Logic;
using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Logic.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IPortfolioLogic _portfolioLogic;

        public UserCreatedEventHandler(IPortfolioLogic portfolioLogic)
        {
            _portfolioLogic = portfolioLogic;
        }

        public async Task HandleAsync(UserCreatedEvent userCreatedEvent)
        {
            await _portfolioLogic.CreatePortfolioForUserAsync(userCreatedEvent.UserId);
        }
    }
}
