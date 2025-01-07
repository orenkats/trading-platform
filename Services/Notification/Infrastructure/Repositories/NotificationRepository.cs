using NotificationService.Domain.Entities;

namespace NotificationService.Infrastracture.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly List<Notification> _notifications = new(); // Mock database for simplicity

    public Task AddAsync(Notification notification)
    {
        _notifications.Add(notification);
        return Task.CompletedTask;
    }
}
