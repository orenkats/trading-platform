using System.Threading.Tasks;
using Shared.Events;
using Shared.Messaging;

namespace NotificationsService.EventHandlers;

public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(UserCreatedEvent @event)
    {
        try
        {
            // Log the receipt of the event
            _logger.LogInformation("Received UserCreatedEvent: {EventId} for User: {UserId}", @event.EventId, @event.UserId);

            // Simulate sending a welcome email by logging to the console
            _logger.LogInformation("Simulating Welcome Email Sending...");
            _logger.LogInformation("To: {Email}", @event.Email);
            _logger.LogInformation("Subject: Welcome to Trading Platform!");
            _logger.LogInformation("Message: Hi {Name}, Welcome to Trading Platform! We're excited to have you on board.", @event.Name);

            // Simulate a slight delay to mimic asynchronous behavior
            await Task.Delay(500);

            // Log successful processing
            _logger.LogInformation("Successfully processed UserCreatedEvent: {EventId}", @event.EventId);
        }
        catch (Exception ex)
        {
            // Log the error
            _logger.LogError(ex, "Error processing UserCreatedEvent: {EventId} for User: {UserId}", @event.EventId, @event.UserId);

            // Re-throw to ensure proper handling in the message pipeline
            throw;
        }
    }
}
