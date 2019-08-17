using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ISmsSender
    {
        Task SendAsync(string phone, string message);
    }
}