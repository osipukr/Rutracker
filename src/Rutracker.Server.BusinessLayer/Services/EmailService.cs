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

        public async Task SendConfirmationEmailAsync(string email, string url)
        {
            CheckParameters(email, url);

            await _emailSender.SendAsync(
                email: email,
                subject: "Rutracker | Confirm your email",
                message: $"Confirm your email by following this {GenerateHtmlLink(url, "link")}");
        }

        public async Task SendEmailChangeConfirmationAsync(string email, string url)
        {
            CheckParameters(email, url);

            await _emailSender.SendAsync(
                email: email,
                subject: "Rutracker | Confirm change email",
                message: $"Confirm your email change by following this {GenerateHtmlLink(url, "link")}");
        }

        public async Task SendResetPasswordAsync(string email, string url)
        {
            CheckParameters(email, url);

            await _emailSender.SendAsync(
                email: email,
                subject: "Rutracker | Reset Password",
                message: $"Reset password by following this {GenerateHtmlLink(url, "link")}");
        }

        private static void CheckParameters(string email, string url)
        {
            Guard.Against.NullOrWhiteSpace(email, message: "Invalid email.");
            Guard.Against.NullOrWhiteSpace(url, message: "Invalid url.");
        }

        private static string GenerateHtmlLink(string url, string message) => $"<a href='{url}' target='_blank'>{message}</a>";
    }
}