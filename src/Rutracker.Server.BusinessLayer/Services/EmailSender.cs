using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailOptions)
        {
            _emailSettings = emailOptions.Value;

            Guard.Against.Null(_emailSettings, nameof(_emailSettings));
            Guard.Against.NullOrWhiteSpace(_emailSettings.SenderEmail, parameterName: nameof(_emailSettings.SenderEmail));
            Guard.Against.NullOrWhiteSpace(_emailSettings.SenderEmailPassword, parameterName: nameof(_emailSettings.SenderEmailPassword));
            Guard.Against.NullOrWhiteSpace(_emailSettings.SmtpServer, parameterName: nameof(_emailSettings.SmtpServer));
            Guard.Against.OutOfRange(_emailSettings.SmtpServerPort, nameof(_emailSettings.SmtpServerPort), 1, int.MaxValue);
        }

        public async Task SendAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Rutracker", "no-reply@rutracker.com"));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpServerPort, useSsl: true);
            await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderEmailPassword);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(quit: true);
        }
    }
}