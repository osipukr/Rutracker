using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Infrastructure.Services.Options;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Rutracker.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailAuthOptions _emailAuthOptions;

        public EmailSender(IOptions<EmailAuthOptions> emailOptions)
        {
            _emailAuthOptions = emailOptions?.Value ?? throw new ArgumentNullException(nameof(emailOptions));
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Fredstone", "no-reply@rutracker.com"));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(_emailAuthOptions.SmtpServer, _emailAuthOptions.SmtpServerPort, useSsl: true);
            await client.AuthenticateAsync(_emailAuthOptions.SenderEmail, _emailAuthOptions.SenderEmailPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(quit: true);
        }
    }
}