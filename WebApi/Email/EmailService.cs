using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using WebApi.Email;

namespace WebApi.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ILogger<EmailService> _logger;

        public EmailService(EmailConfiguration emailConfig, ILogger<EmailService> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            email.To.Add(new MailboxAddress(to, to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true; // Ignorar validação do certificado
                _logger.LogInformation("Connecting to SMTP server...");
                await smtp.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                _logger.LogInformation("Authenticating...");
                await smtp.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
                _logger.LogInformation("Sending email to {Email}", to);
                await smtp.SendAsync(email);
                _logger.LogInformation("Email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email.");
                throw;
            }
            finally
            {
                _logger.LogInformation("Disconnecting from SMTP server...");
                await smtp.DisconnectAsync(true);
                smtp.Dispose();
            }
        }
    }
}
