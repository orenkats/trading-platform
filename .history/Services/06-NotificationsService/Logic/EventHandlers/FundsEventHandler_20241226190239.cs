using Shared.Events;
using Shared.Messaging;
using NotificationsService.Data.Entities;
using NotificationsService.Data.Repositories;

namespace NotificationsService.Logic.EventHandlers;

public class FundsEventHandler : IEventHandler<object>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationLogic _notificationLogic;

    public FundsEventHandler(INotificationRepository notificationRepository, INotificationLogic notificationLogic)
    {
        _notificationRepository = notificationRepository;
        _notificationLogic = notificationLogic;
    }

    public async Task HandleAsync(object @event)
    {
        switch (@event)
        {
            case FundsDepositedEvent depositedEvent:
                var depositMessage = $"Dear User, your deposit of {depositedEvent.Amount:C} was successful.";
                await _notificationLogic.SendEmailAsync("user@example.com", "Deposit Notification", depositMessage);
                await _notificationRepository.AddAsync(new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = depositedEvent.UserId,
                    Email = "user@example.com",
                    Subject = "Deposit Notification",
                    Message = depositMessage,
                    CreatedAt = DateTime.UtcNow
                });
                break;

            case FundsWithdrawnEvent withdrawnEvent:
                var withdrawalMessage = $"Dear User, your withdrawal of {withdrawnEvent.Amount:C} was successful.";
                await _notificationLogic.SendEmailAsync("user@example.com", "Withdrawal Notification", withdrawalMessage);
                await _notificationRepository.AddAsync(new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = withdrawnEvent.UserId,
                    Email = "user@example.com",
                    Subject = "Withdrawal Notification",
                    Message = withdrawalMessage,
                    CreatedAt = DateTime.UtcNow
                });
                break;

            default:
                throw new InvalidOperationException("Unhandled event type");
        }
    }
}
