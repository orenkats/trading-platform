using MimeKit;
using MailKit.Net.Smtp;

namespace NotificationsService.Logic;

public class NotificationLogic : INotificationLogic
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Trading Platform", "no-reply@tradingplatform.com"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.example.com", 587, false); // Replace with actual SMTP server
        await client.AuthenticateAsync("username", "password"); // Replace with actual credentials
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
