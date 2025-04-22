using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Porfolio.Web.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailOptions _options;

    public EmailService(IOptions<EmailOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(string fromName, string fromEmail, string messageText)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(fromName, fromEmail));
        email.To.Add(MailboxAddress.Parse(_options.ToEmail));
        email.Subject = "Mensaje desde el formulario de contacto";
        email.Body = new TextPart("plain")
        {
            Text = messageText
        };

        using var smtp = new SmtpClient(); // Ensure this is MailKit.Net.Smtp.SmtpClient
        
        await smtp.ConnectAsync(
            _options.SmtpHost,
            _options.SmtpPort!,
            SecureSocketOptions.StartTls     
        );

        await smtp.AuthenticateAsync(
            _options.User,
            _options.Password
        );

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
