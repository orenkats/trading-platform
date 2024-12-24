using Microsoft.AspNetCore.Mvc.ActionConstraints;
using PortfolioService.Data.Entities;
using PortfolioService.Logic;
using Shared.Events;
using Shared.Messaging;

namespace PortfolioService.Logic.EventHandlers
{
    public class UserCreatedEventHandler
    {
        private readonly IPortfolioLogic _portfolioLogic;
        private readonly IEventBus _eventBus;

        public UserCreatedEventHandler(IPortfolioLogic portfolioLogic, IEventBus eventBus)
        {
            _portfolioLogic = portfolioLogic;
            _eventBus = eventBus;
        }

        public async Task HandleUserCreatedEventAsync(UserCreatedEvent userCreatedEvent)
        {
            var portfolio = await _portfolioLogic.CreatePortfolioForUserAsync(userCreatedEvent.UserId);

            var portfolioCreatedEvent = new PortfolioCreatedEvent
            {
                PortfolioId = portfolio.Id,
                UserId = userCreatedEvent.UserId
            };

            _eventBus.Publish(portfolioCreatedEvent, "PortfolioExchange");
        }
    }
}
