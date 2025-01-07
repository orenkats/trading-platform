using PortfolioService.Application.Commands;
using Shared.Events;
using Shared.Messaging;
using Microsoft.Extensions.Logging;

namespace PortfolioService.Application.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly CreatePortfolioCommand _createPortfolioCommand;
        private readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(
            CreatePortfolioCommand createPortfolioCommand,
            ILogger<UserCreatedEventHandler> logger)
        {
            _createPortfolioCommand = createPortfolioCommand;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedEvent userCreatedEvent)
        {
            _logger.LogInformation("Handling UserCreatedEvent for UserId: {UserId}", userCreatedEvent.UserId);

            try
            {
                await _createPortfolioCommand.ExecuteAsync(userCreatedEvent.UserId);

                _logger.LogInformation("Successfully created portfolio for UserId: {UserId}", userCreatedEvent.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to handle UserCreatedEvent for UserId: {UserId}", userCreatedEvent.UserId);
                throw;
            }
        }
    }
}
