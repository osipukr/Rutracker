using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.Interfaces;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Controllers
{
    /// <summary>
    ///     The Torrent API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="404">If the item is null.</response>
    public class TorrentsController : BaseController, ITorrentsController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService) =>
            _torrentViewModelService = torrentViewModelService;

        /// <summary>
        ///     Get all items on the page with information for pagination.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="filter">Information to filter elements.</param>
        [HttpPost("pagination")]
        public async Task<TorrentsIndexViewModel> Pagination(int page, int pageSize, FiltrationViewModel filter) =>
            await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

        /// <summary>
        ///     Get information about the item.
        /// </summary>
        /// <param name="id">ID of the element.</param>
        [HttpGet]
        public async Task<TorrentIndexViewModel> Get(long id) =>
            await _torrentViewModelService.GetTorrentIndexAsync(id);

        /// <summary>
        ///     Get information about the facet for the title.
        /// </summary>
        /// <param name="count">Number of elements.</param>
        [HttpGet("titles")]
        public async Task<FacetViewModel<string>> Titles(int count) =>
            await _torrentViewModelService.GetTitleFacetAsync(count);
    }
}