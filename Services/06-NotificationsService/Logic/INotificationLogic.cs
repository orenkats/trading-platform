namespace NotificationsService.Logic;

public interface INotificationLogic
{
    Task SendEmailAsync(string email, string subject, string message);
}
