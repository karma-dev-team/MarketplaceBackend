namespace KarmaMarketplace.Infrastructure.Adapters.Mailing
{
    using MailKit.Security;
    using MimeKit;
    using System.Threading.Tasks;

    public class MailKitEmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _username;
        private readonly string _password;

        public MailKitEmailService(string smtpServer, int smtpPort, string fromEmail, string username, string password)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _fromEmail = fromEmail;
            _username = username;
            _password = password;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_fromEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                await smtp.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_username, _password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }

}
