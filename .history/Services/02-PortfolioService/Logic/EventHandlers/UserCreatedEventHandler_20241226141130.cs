using PortfolioService.Data.Entities;
using PortfolioService.Logic;
using Shared.Messaging;
using Shared.Events;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Logic.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IPortfolioLogic _portfolioLogic;
        private readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(
            IPortfolioLogic portfolioLogic,
            ILogger<UserCreatedEventHandler> logger)
        {
            _portfolioLogic = portfolioLogic;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedEvent userCreatedEvent)
        {
            _logger.LogInformation($"Handling UserCreatedEvent for UserId: {userCreatedEvent.UserId}");

            try
            {
                // Fetch or create the user's portfolio
                var portfolio = await _portfolioLogic.GetPortfolioByUserIdAsync(userCreatedEvent.UserId);
                if (portfolio == null)
                {
                    _logger.LogInformation($"No portfolio found for UserId: {userCreatedEvent.UserId}. Creating a new portfolio.");

                    portfolio = new Portfolio
                    {
                        Id = Guid.NewGuid(),
                        UserId = userCreatedEvent.UserId,
                        CreatedAt = DateTime.UtcNow,
                        AccountBalance = 0 // Default account balance
                    };

                    // Create the portfolio for the user
                    await _portfolioLogic.CreatePortfolioAsync(portfolio);
                }

                _logger.LogInformation($"Portfolio created or verified successfully for UserId: {userCreatedEvent.UserId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error handling UserCreatedEvent. UserId: {userCreatedEvent.UserId}. Exception: {ex.Message}");
                throw;
            }
        }
    }
}
