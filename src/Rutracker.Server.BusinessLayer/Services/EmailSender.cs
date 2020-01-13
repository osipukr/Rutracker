using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;
using Rutracker.Server.BusinessLayer.Properties;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class EmailSender : IEmailSender
    {
        private const string EMAIL_NAME = "Rutracker";
        private const string EMAIL_ADDRESS = "no-reply@rutracker.com";

        private readonly EmailAuthOptions _emailSettings;

        public EmailSender(IOptions<EmailAuthOptions> emailOptions)
        {
            _emailSettings = emailOptions.Value;
        }

        public async Task SendAsync(string email, string subject, string message)
        {
            Guard.Against.IsValidEmail(email, Resources.EmailSender_InvalidEmailAddress);
            Guard.Against.NullString(subject, Resources.EmailSender_InvalidSubjectName);
            Guard.Against.NullString(message, Resources.EmailSender_InvalidMessage);

            var emailMessage = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart(TextFormat.Html)
                {
                    Text = message
                }
            };

            emailMessage.From.Add(new MailboxAddress(EMAIL_NAME, EMAIL_ADDRESS));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));

            using var client = new SmtpClient();

            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpServerPort, useSsl: true);
            await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderEmailPassword);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(quit: true);
        }
    }
}