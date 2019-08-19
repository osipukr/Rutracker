using System;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
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
            await _emailSender.SendAsync(
                email: email,
                subject: "Confirm your account",
                message: $"Confirm your email by following this link: <a href='{confirmationUrl}'>link</a>");
        }

        public async Task SendEmailChangeConfirmation(string email, string confirmationUrl)
        {
            await _emailSender.SendAsync(
                email: email,
                subject: "Confirm change email",
                message: $"Confirm your email change by following this link: <a href='{confirmationUrl}'>link</a>");
        }
    }
}