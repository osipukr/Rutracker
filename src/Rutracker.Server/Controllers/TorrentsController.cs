using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;

namespace Rutracker.Server.Controllers
{
    public class TorrentsController : BaseController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService) => _torrentViewModelService = torrentViewModelService;

        [Route("paging")]
        [HttpGet("{page:int}/{pageSize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TorrentIndexViewModel>> GetTorrentsIndexAsync(int page, int pageSize, [FromBody] FiltrationViewModel filter)
        {
            try
            {
                var result = await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TorrentIndexViewModel>> GetTorrentIndexAsync(long id)
        {
            try
            {
                var result = await _torrentViewModelService.GetTorrentIndexAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("titles")]
        [HttpGet("{count:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FacetItemViewModel[]>> GetTitlesAsync(int count)
        {
            try
            {
                var result = await _torrentViewModelService.GetTitlesAsync(count);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}