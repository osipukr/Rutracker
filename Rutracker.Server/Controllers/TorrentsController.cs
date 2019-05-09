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

        [HttpGet("{search?}/{sort?}/{order?}/{pageid?}")]
        public async Task<IActionResult> GetTorrentsAsync(string search = "", string sort = "Id", string order = "Asc", int pageid = 1)
        {
            var result =
                await _torrentService.GetTorrentsAsync(pageid, Constants.ItemsPerPage, search, sort, order);

            return Ok(result);
        }

        [HttpGet("{torrentid}")]
        public async Task<IActionResult> GetTorrentAsync(int torrentid)
        {
            var result = await _torrentService.GetTorrentAsync(torrentid);

            return Ok(result);
        }
    }
}