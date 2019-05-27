using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;

namespace Rutracker.Server.Controllers
{
    public class TorrentController : BaseController
    {
        private readonly ITorrentService _torrentService;

        public TorrentController(ITorrentService torrentService)
        {
            _torrentService = torrentService;
        }

        [HttpGet("{pageSize}/{page}/{search?}")]
        public async Task<IActionResult> GetTorrentsAsync(int pageSize, int page, string search,
            [FromQuery] FiltrationViewModel filter)
        {
            var result = await _torrentService.GetTorrentsAsync(page, pageSize, search, filter);

            return Ok(result);
        }

        [Route("details")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTorrentAsync(long id)
        {
            var result = await _torrentService.GetTorrentAsync(id);

            return Ok(result);
        }

        [Route("forums")]
        [HttpGet("{count}")]
        public async Task<IActionResult> GetTorrentFilterAsync(int count)
        {
            var result = await _torrentService.GetTorrentFilterAsync(count);

            return Ok(result);
        }
    }
}