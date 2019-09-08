using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Server.BusinessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationUrl)
        {
            Guard.Against.NullOrWhiteSpace(email, message: "Invalid email.");
            Guard.Against.NullOrWhiteSpace(confirmationUrl, message: "Invalid confirmation url.");

            await _emailSender.SendAsync(
                email: email,
                subject: "Confirm your email",
                message: $"Confirm your email by following this <a href='{confirmationUrl}'>link</a>");
        }

        public async Task SendEmailChangeConfirmation(string email, string confirmationUrl)
        {
            Guard.Against.NullOrWhiteSpace(email, message: "Invalid email.");
            Guard.Against.NullOrWhiteSpace(confirmationUrl, message: "Invalid confirmation url.");

            await _emailSender.SendAsync(
                email: email,
                subject: "Confirm change email",
                message: $"Confirm your email change by following this <a href='{confirmationUrl}'>link</a>");
        }
    }
}