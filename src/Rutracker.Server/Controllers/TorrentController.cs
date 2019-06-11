﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;

namespace Rutracker.Server.Controllers
{
    public class TorrentController : BaseController
    {
        private readonly ITorrentService _torrentService;

        public TorrentController(ITorrentService torrentService)
        {
            _torrentService = torrentService;
        }

        [Route("torrents")]
        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetTorrentsAsync(int page, int pageSize, [FromBody] FiltrationViewModel filter)
        {
            try
            {
                var result = await _torrentService.GetTorrentsIndexAsync(page, pageSize, filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("details")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTorrentAsync(long id)
        {
            try
            {
                var result = await _torrentService.GetTorrentIndexAsync(id);

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
                var result = await _torrentService.GetTitlesAsync(count);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}