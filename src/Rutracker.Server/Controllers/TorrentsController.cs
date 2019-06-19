using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;

namespace Rutracker.Server.Controllers
{
    public class TorrentsController : BaseController
    {
        private readonly ITorrentViewModelService _torrentViewModelService;

        public TorrentsController(ITorrentViewModelService torrentViewModelService) =>
            _torrentViewModelService = torrentViewModelService;

        [Route("paging")]
        [HttpGet("{page}/{pageSize}", Name = "GetTorrentsIndexAsync")]
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

        [Route("")]
        [HttpGet("{id}", Name = "GetTorrentIndexAsync")]
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
        [HttpGet("{count}", Name = "GetTitlesAsync")]
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