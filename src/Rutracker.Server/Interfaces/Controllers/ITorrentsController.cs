using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Shared.ViewModels.Shared;

namespace Rutracker.Server.Interfaces.Controllers
{
    public interface ITorrentsController
    {
        Task<IActionResult> Pagination(int page, int pageSize, FiltrationViewModel filter);
        Task<IActionResult> Get(long id);
        Task<IActionResult> Titles(int count);
    }
}