using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces.Controllers;
using Rutracker.Server.Interfaces.Services;
using Rutracker.Shared.Resources.Controllers;
using Rutracker.Shared.ViewModels.Shared;

namespace Rutracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class TorrentsController : ControllerBase, ITorrentsController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService) =>
            _torrentViewModelService = torrentViewModelService;

        [HttpPost("pagination")]
        public async Task<IActionResult> Pagination(
            [Range(minimum: 1, maximum: int.MaxValue,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.PageError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] int page,

            [Range(minimum: 1, maximum: 100,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.PageSizeError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] int pageSize,

            [FromBody] FiltrationViewModel filter)
        {
            var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [Range(minimum: 1, maximum: long.MaxValue,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.TorrentIdError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] long id)
        {
            var result = await _torrentViewModelService.GetTorrentIndexAsync(id);

            return Ok(result);
        }

        [HttpGet("titles")]
        public async Task<IActionResult> Titles(
            [Range(minimum: 1, maximum: int.MaxValue,
                ErrorMessageResourceName = nameof(TorrentsControllerResource.TitlesCountError),
                ErrorMessageResourceType = typeof(TorrentsControllerResource))] int count)
        {
            var result = await _torrentViewModelService.GetTitleFacetAsync(count);

            return Ok(result);
        }
    }
}