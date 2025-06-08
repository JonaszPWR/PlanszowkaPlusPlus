using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace PlanszowkaPlusPlus.Services
{
    public class EmailService
    {
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string fromEmail = "planszowka.app@gmail.com";        
        private readonly string password = "bbjz utgu jffe xeif";     

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Planszówka Klub", fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(fromEmail, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
