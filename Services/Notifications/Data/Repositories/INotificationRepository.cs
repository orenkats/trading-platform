using NotificationsService.Data.Entities;

namespace NotificationsService.Data.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);
}
