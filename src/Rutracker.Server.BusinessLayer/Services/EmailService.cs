using System;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

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
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new RutrackerException("Not valid email.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(confirmationUrl))
            {
                throw new RutrackerException("Not valid confirmation url.", ExceptionEventType.NotValidParameters);
            }

            await _emailSender.SendAsync(
                email: email,
                subject: "Confirm your account",
                message: $"Confirm your email by following this link: <a href='{confirmationUrl}'>link</a>");
        }
    }
}