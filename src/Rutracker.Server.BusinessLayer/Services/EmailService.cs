using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;

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
            var message = string.Format(Resources.EmailService_ConfirmEmailMessage, url);

            await _emailSender.SendAsync(email, Resources.EmailService_ConfirmEmailSubjectMessage, message);
        }

        public async Task SendEmailChangeConfirmationAsync(string email, string url)
        {
            var message = string.Format(Resources.EmailService_ConfirmChangeEmailMessage, url);

            await _emailSender.SendAsync(email, Resources.EmailService_ConfirmChangeEmailSubjectMessage, message);
        }

        public async Task SendResetPasswordAsync(string email, string url)
        {
            var message = string.Format(Resources.EmailService_ResetPasswordEmailMessage, url);

            await _emailSender.SendAsync(email, Resources.EmailService_ResetPasswordEmailSubject, message);
        }
    }
}