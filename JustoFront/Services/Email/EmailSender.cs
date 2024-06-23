using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JustoFront.Services.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var mail = "yann_cardozo@hotmail.com";
            var pw = "Chaons26196460!@";

            var client = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            return client.SendMailAsync(mailMessage);
        }
    }
}