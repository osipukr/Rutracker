using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IEmailService
    {
        Task SendResetPasswordAsync(string email, string url);
    }
}