using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;

namespace Rutracker.Server.Controllers
{
    public class TorrentsController : BaseController
    {
        private readonly ITorrentService _torrentService;

        public TorrentsController(ITorrentService torrentService)
        {
            _torrentService = torrentService;
        }

        [HttpGet("{pageSize}/{page}/{search?}")]
        public async Task<IActionResult> GetTorrentsAsync(string search, int pageSize, int page)
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
    }
}