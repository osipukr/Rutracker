using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Shared;

namespace Rutracker.Server.Controllers
{
    public class TorrentsController : BaseController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService) =>
            _torrentViewModelService = torrentViewModelService;

        [Route(nameof(Pagination))]
        public async Task<IActionResult> Pagination(int page, int pageSize, [FromBody] FiltrationViewModel filter)
        {
            var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

            return Ok(result);
        }

        [Route("")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _torrentViewModelService.GetTorrentIndexAsync(id);

            return Ok(result);
        }

        [Route(nameof(Titles))]
        public async Task<IActionResult> Titles(int count)
        {
            var result = await _torrentViewModelService.GetTitleFacetAsync(count);

            return Ok(result);
        }
    }
}