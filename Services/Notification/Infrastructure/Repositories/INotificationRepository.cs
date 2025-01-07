using NotificationService.Domain.Entities;

namespace NotificationService.Infrastracture.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);
}
