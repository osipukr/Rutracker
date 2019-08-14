using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The Torrent API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="404">If the item is null.</response>
    public class TorrentsController : BaseApiController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService)
        {
            _torrentViewModelService = torrentViewModelService ?? throw new ArgumentNullException(nameof(torrentViewModelService));
        }

        /// <summary>
        ///     Get all items on the page with information for pagination.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="filter">Information to filter elements.</param>
        [HttpPost(nameof(Pagination))]
        public async Task<PaginationResult<TorrentViewModel>> Pagination(int page, int pageSize, FilterViewModel filter) =>
            await _torrentViewModelService.TorrentsAsync(page, pageSize, filter);

        /// <summary>
        ///     Get information about the item.
        /// </summary>
        /// <param name="id">ID of the element.</param>
        [HttpGet]
        public async Task<TorrentDetailsViewModel> Get(long id) => await _torrentViewModelService.TorrentAsync(id);

        /// <summary>
        ///     Get information about the facet for the title.
        /// </summary>
        /// <param name="count">Number of elements.</param>
        [HttpGet(nameof(Titles))]
        public async Task<FacetResult<string>> Titles(int count) => await _torrentViewModelService.ForumFacetAsync(count);
    }
}