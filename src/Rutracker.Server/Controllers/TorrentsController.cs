using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces.Controllers;
using Rutracker.Server.Interfaces.Services;
using Rutracker.Shared.ViewModels.Shared;

namespace Rutracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class TorrentsController : ControllerBase, ITorrentsController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService) =>
            _torrentViewModelService = torrentViewModelService;

        [HttpPost("pagination")]
        public async Task<IActionResult> Pagination(
            [Range(1, int.MaxValue)] int page,
            [Range(1, 100)] int pageSize,
            [FromBody] FiltrationViewModel filter)
        {
            var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([Range(1, long.MaxValue)] long id)
        {
            var result = await _torrentViewModelService.GetTorrentIndexAsync(id);

            return Ok(result);
        }

        [HttpGet("titles")]
        public async Task<IActionResult> Titles([Range(1, int.MaxValue)] int count)
        {
            var result = await _torrentViewModelService.GetTitleFacetAsync(count);

            return Ok(result);
        }
    }
}