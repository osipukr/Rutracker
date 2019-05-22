using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;

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
        public async Task<IActionResult> GetTorrentsAsync(int pageSize, int page, string search)
        {
            var result = await _torrentService.GetTorrentsAsync(page, pageSize, search);

            return Ok(result);
        }

        [Route("details")]
        [HttpGet("{torrentid}")]
        public async Task<IActionResult> GetTorrentAsync(long torrentid)
        {
            var result = await _torrentService.GetTorrentAsync(torrentid);

            return Ok(result);
        }

        [Route("forums")]
        [HttpGet("{count}")]
        public async Task<IActionResult> GetFiltrationAsync(int count)
        {
            var result = await _torrentService.GetFiltrationAsync(count);

            return Ok(result);
        }
    }
}