using System;
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

        [Route(nameof(Paging))]
        public async Task<IActionResult> Paging(int page, int pageSize, [FromBody] FiltrationViewModel filter)
        {
            object response;

            try
            {
                var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

                response = new OkResponse<TorrentsIndexViewModel>(result);
            }
            catch (Exception ex)
            {
                response = new BadRequestResponse(ex.Message);
            }

            return Ok(response);
        }

        [Route("")]
        public async Task<IActionResult> Get(long id)
        {
            object response;

            try
            {
                var result = await _torrentViewModelService.GetTorrentIndexAsync(id);

                response = new OkResponse<TorrentIndexViewModel>(result);
            }
            catch (Exception ex)
            {
                response = new BadRequestResponse(ex.Message);
            }

            return Ok(response);
        }

        [Route(nameof(Titles))]
        public async Task<IActionResult> Titles(int count)
        {
            object response;

            try
            {
                var result = await _torrentViewModelService.GetTitleFacetAsync(count);

                response = new OkResponse<FacetViewModel>(result);
            }
            catch (Exception ex)
            {
                response = new BadRequestResponse(ex.Message);
            }

            return Ok(response);
        }
    }
}