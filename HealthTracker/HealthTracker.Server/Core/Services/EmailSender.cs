using System.Net.Mail;
using System.Net;

namespace HealthTracker.Server.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient("HealthTracker.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("HealthTracker@example.com", "password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@example.com"),
                Subject = subject,
                Body = "Test message",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            return smtpClient.SendMailAsync(mailMessage);
        }
    }
}
