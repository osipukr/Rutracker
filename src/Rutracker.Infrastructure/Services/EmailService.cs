using System;
using System.Threading.Tasks;
using Rutracker.Core.Interfaces.Services;

namespace Rutracker.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationUrl)
        {
            await _emailSender.SendEmailAsync(email,
                subject: "Confirm your account",
                message: $"Confirm your email by following this link: <a href='{confirmationUrl}'>link</a>");
        }
    }
}