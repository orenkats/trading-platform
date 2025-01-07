namespace NotificationService.Domain.Interfaces;

public interface INotificationDomainService
{
    Task SendEmailAsync(string email, string subject, string message);
}
