using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(string email, string subject, string message);
    }
}