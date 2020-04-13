using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ISmsService
    {
        Task SendPhoneConfirmationAsync(string phone, string code);
    }
}