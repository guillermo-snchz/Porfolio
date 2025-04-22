
namespace Porfolio.Web.Services.Email;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string to, string subject, string body)
    {
        // Simulate sending an email
        _logger.LogInformation($"Sending email to {to} with subject '{subject}' and body '{body}'");

        // In a real application, you would use an SMTP client or an email service provider API to send the email.
        // For example, using SmtpClient or a third-party library like MailKit.
        return Task.CompletedTask;
    }
}
