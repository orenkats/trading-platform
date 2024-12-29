using PortfolioService.Application.Services;
using PortfolioService.Domain.Entities;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IPortfolioAppService _appService;
        private readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(
            IPortfolioAppService appService,
            ILogger<UserCreatedEventHandler> logger)
        {
            _appService = appService;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedEvent userCreatedEvent)
        {
            _logger.LogInformation("Handling UserCreatedEvent for UserId: {UserId}", userCreatedEvent.UserId);

            try
            {
                // Create a new portfolio for the user
                var newPortfolio = new Portfolio
                {
                    UserId = userCreatedEvent.UserId,
                    CreatedAt = DateTime.UtcNow,
                    AccountBalance = 0 // Default balance
                };

                await _appService.DepositFundsAsync(newPortfolio.UserId, newPortfolio.AccountBalance);

                _logger.LogInformation("Successfully handled UserCreatedEvent for UserId: {UserId}", userCreatedEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to handle UserCreatedEvent for UserId: {UserId}", userCreatedEvent.UserId);
                throw;
            }
        }
    }
}
