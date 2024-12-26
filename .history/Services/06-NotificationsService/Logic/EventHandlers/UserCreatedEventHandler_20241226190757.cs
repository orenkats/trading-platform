using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Shared.Events;
using Shared.Messaging;

namespace NotificationsService.Logic.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        public async Task HandleAsync(UserCreatedEvent @event)
        {
            // Compose the welcome email
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Trading Platform", "no-reply@tradingplatform.com"));
            message.To.Add(new MailboxAddress(@event.Name, @event.Email));
            message.Subject = "Welcome to Trading Platform!";
            message.Body = new TextPart("plain")
            {
                Text = $"Hi {@event.Name},\n\nWelcome to Trading Platform! We're excited to have you on board.\n\nBest regards,\nThe Trading Platform Team"
            };

            // Send the email
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.your-email-provider.com", 587, false); // Replace with your SMTP server and port
                    await client.AuthenticateAsync("your-email@example.com", "your-email-password"); // Replace with your email and password
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions (log them for debugging)
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
