using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Rutracker.Server.BusinessLayer.Collections;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Collections;
using Microsoft.Net.Http.Headers;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The Torrent API controller.
    /// </summary>
    [Authorize(Policy = Policies.IsAdmin)]
    public class TorrentsController : ApiController
    {
        private readonly ITorrentService _torrentService;

        public TorrentsController(ITorrentService torrentService, IMapper mapper) : base(mapper)
        {
            _torrentService = torrentService;
        }

        /// <summary>
        ///     Get all items on the page with information for pagination.
        /// </summary>
        /// <param name="filter"></param>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IPagedList<TorrentView>> Get([FromQuery] TorrentFilter filter)
        {
            var pagedList = await _torrentService.ListAsync(filter);

            return PagedList.From(pagedList, torrents => _mapper.Map<IEnumerable<TorrentView>>(torrents));
        }

        /// <summary>
        ///     Get information about the item.
        /// </summary>
        /// <param name="id">ID of the element.</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<TorrentView> Get(int id)
        {
            var torrent = await _torrentService.FindAsync(id);

            return _mapper.Map<TorrentView>(torrent);
        }

        [HttpPost]
        public async Task<TorrentView> Post(TorrentCreateView model)
        {
            var torrent = _mapper.Map<Torrent>(model);

            var result = await _torrentService.AddAsync(torrent);

            return _mapper.Map<TorrentView>(result);
        }

        [HttpPost("stock")]
        public async Task<TorrentView> Post(TorrentStockCreateView model)
        {
            var torrent = _mapper.Map<Torrent>(model);

            var result = await _torrentService.AddStockAsync(torrent);

            return _mapper.Map<TorrentView>(result);
        }

        [HttpPut("{id}")]
        public async Task<TorrentView> Put(int id, TorrentUpdateView model)
        {
            var torrent = _mapper.Map<Torrent>(model);

            var result = await _torrentService.UpdateAsync(id, torrent);

            return _mapper.Map<TorrentView>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _torrentService.DeleteAsync(id);
        }

        [AllowAnonymous]
        [HttpGet("{id}/download")]
        public async Task Download(int id)
        {
            var fileName = await _torrentService.GetDownloadFileName(id);

            Response.ContentType = MediaTypeNames.Application.Octet;
            Response.Headers.Add("Content-Disposition", new ContentDisposition
            {
                FileName = fileName,
                DispositionType = "attachment"
            }.ToString());

            await _torrentService.DownloadToStreamAsync(id, Response.Body);

            await Response.Body.FlushAsync();
        }
    }
}