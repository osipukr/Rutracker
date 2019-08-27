using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ISmsService
    {
        Task SendConfirmationPhoneAsync(string phone, string code);
    }
}