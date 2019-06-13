using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Services.Types;
using Rutracker.Shared.ViewModels;

namespace Rutracker.Server.Controllers
{
    public class TorrentController : BaseController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentController(Func<TorrentViewModelServiceEnum, ITorrentViewModelService> serviceResolver) =>
            _torrentViewModelService = serviceResolver(TorrentViewModelServiceEnum.Cached);

        [Route("torrents")]
        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetTorrentsIndexAsync(int page, int pageSize, [FromBody] FiltrationViewModel filter)
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

        [Route("details")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTorrentIndexAsync(long id)
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
        [HttpGet("{count}")]
        public async Task<IActionResult> GetTitlesAsync(int count)
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