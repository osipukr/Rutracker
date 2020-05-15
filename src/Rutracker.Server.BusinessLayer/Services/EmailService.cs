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

        public async Task SendResetPasswordAsync(string email, string url)
        {
            var message = string.Format(Resources.EmailService_ResetPasswordEmailMessage, url);

            await _emailSender.SendAsync(email, Resources.EmailService_ResetPasswordEmailSubject, message);
        }
    }
}