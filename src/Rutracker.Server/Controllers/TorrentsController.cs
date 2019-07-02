using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Response;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

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
            var response = new OkResponse<TorrentsIndexViewModel>(result);

            return Ok(response);
        }

        [Route("")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _torrentViewModelService.GetTorrentIndexAsync(id);
            var response = new OkResponse<TorrentIndexViewModel>(result);

            return Ok(response);
        }

        [Route(nameof(Titles))]
        public async Task<IActionResult> Titles(int count)
        {
            var result = await _torrentViewModelService.GetTitleFacetAsync(count);
            var response = new OkResponse<FacetViewModel>(result);

            return Ok(response);
        }
    }
}