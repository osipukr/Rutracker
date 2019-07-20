using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rutracker.Server.Interfaces.Controllers;
using Rutracker.Server.Interfaces.Services;
using Rutracker.Shared.Resources.Controllers;
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
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public class TorrentsController : ControllerBase, ITorrentsController
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
        [ProducesResponseType(typeof(TorrentsIndexViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Pagination(
            [Range(minimum: 1, maximum: int.MaxValue,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.PageError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] int page,

            [Range(minimum: 1, maximum: 100,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.PageSizeError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] int pageSize,

            [FromBody] FiltrationViewModel filter) =>
            Ok(await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter));

        /// <summary>
        ///     Get information about the item.
        /// </summary>
        /// <param name="id">ID of the element.</param>
        [HttpGet]
        [ProducesResponseType(typeof(TorrentIndexViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [Range(minimum: 1, maximum: long.MaxValue,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.TorrentIdError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] long id) =>
            Ok(await _torrentViewModelService.GetTorrentIndexAsync(id));

        /// <summary>
        ///     Get information about the facet for the title.
        /// </summary>
        /// <param name="count">Number of elements.</param>
        [HttpGet("titles")]
        [ProducesResponseType(typeof(FacetViewModel<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Titles(
            [Range(minimum: 1, maximum: int.MaxValue,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.TitlesCountError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] int count) =>
            Ok(await _torrentViewModelService.GetTitleFacetAsync(count));
    }
}