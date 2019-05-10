using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Constants = Rutracker.Shared.Constants;

namespace Rutracker.Server.Controllers
{
    public class TorrentsController : BaseController
    {
        private readonly ITorrentService _torrentService;

        public TorrentsController(ITorrentService torrentService)
        {
            _torrentService = torrentService;
        }

        [HttpGet("{search?}/{sort?}/{order?}/{page?}")]
        public async Task<IActionResult> GetTorrentsAsync(string search, string sort, string order, int page)
        {
            var result =
                await _torrentService.GetTorrentsAsync(page, Constants.ItemsPerPage, search, sort, order);

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