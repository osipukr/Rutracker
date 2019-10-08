using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string url);
        Task SendEmailChangeConfirmationAsync(string email, string url);
        Task SendResetPasswordAsync(string email, string url);
    }
}