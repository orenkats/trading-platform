namespace NotificationsService.Data.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
