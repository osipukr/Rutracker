using System.Collections.Generic;
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
        public async Task<TorrentDetailView> Get(int id)
        {
            var torrent = await _torrentService.FindAsync(id);

            return _mapper.Map<TorrentDetailView>(torrent);
        }

        [HttpPost]
        public async Task<TorrentDetailView> Post(TorrentCreateView model)
        {
            var torrent = _mapper.Map<Torrent>(model);

            var result = await _torrentService.AddAsync(torrent);

            return _mapper.Map<TorrentDetailView>(result);
        }

        [HttpPut("{id}")]
        public async Task<TorrentDetailView> Put(int id, TorrentUpdateView model)
        {
            var torrent = _mapper.Map<Torrent>(model);

            var result = await _torrentService.UpdateAsync(id, torrent);

            return _mapper.Map<TorrentDetailView>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _torrentService.DeleteAsync(id);
        }
    }
}