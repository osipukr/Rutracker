using System.Threading.Tasks;

namespace Rutracker.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmationUrl);
    }
}