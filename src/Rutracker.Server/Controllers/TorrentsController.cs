using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Shared;

namespace Rutracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class TorrentsController : ControllerBase
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService) =>
            _torrentViewModelService = torrentViewModelService;

        [HttpPost("pagination")]
        public async Task<IActionResult> Pagination(int page, int pageSize, [FromBody] FiltrationViewModel filter)
        {
            var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _torrentViewModelService.GetTorrentIndexAsync(id);

            return Ok(result);
        }

        [HttpGet("titles")]
        public async Task<IActionResult> Titles(int count)
        {
            var result = await _torrentViewModelService.GetTitleFacetAsync(count);

            return Ok(result);
        }
    }
}