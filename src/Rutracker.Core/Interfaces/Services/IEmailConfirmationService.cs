using System.Threading.Tasks;

namespace Rutracker.Core.Interfaces.Services
{
    public interface IEmailConfirmationService
    {
        Task ConfirmEmailAsync(string userId, string confirmationCode);
    }
}