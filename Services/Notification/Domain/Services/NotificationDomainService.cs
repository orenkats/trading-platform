
using NotificationService.Domain.Interfaces;

namespace NotificationService.Domain.Services
{
    public class NotificationDomainService : INotificationDomainService
    {
        private readonly ILogger<NotificationDomainService> _logger;

        public NotificationDomainService(ILogger<NotificationDomainService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                // Simulate email sending by logging to the console
                _logger.LogInformation("Simulating email sending...");
                Console.WriteLine("Simulated Email Sending:");
                Console.WriteLine($"To: {email}");
                Console.WriteLine($"Subject: {subject}");
                Console.WriteLine($"Message: {message}");

                // Simulate a slight delay to mimic asynchronous behavior
                await Task.Delay(500);

                _logger.LogInformation("Email simulated successfully for {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to simulate email sending to {Email}", email);
                throw;
            }
        }
    }
}

