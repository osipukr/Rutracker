using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Extensions;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The Torrent API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="404">If the item is null.</response>
    [Authorize(Policy = Policies.IsUser)]
    public class TorrentsController : BaseApiController
    {
        private readonly ITorrentService _torrentService;

        public TorrentsController(ITorrentService torrentService, IMapper mapper) : base(mapper)
        {
            _torrentService = torrentService;
        }

        /// <summary>
        ///     Get all items on the page with information for pagination.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="filter">Information to filter elements.</param>
        [HttpPost("search"), AllowAnonymous]
        public async Task<PaginationResult<TorrentViewModel>> Pagination(int page, int pageSize, TorrentFilterViewModel filter)
        {
            var (torrents, count) = await _torrentService.ListAsync(page, pageSize,
                filter.CategoryId,
                filter.SubcategoryId,
                filter.Search);

            return new PaginationResult<TorrentViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = count,
                Items = _mapper.Map<IEnumerable<TorrentViewModel>>(torrents)
            };
        }

        [HttpGet("popular/{count}"), AllowAnonymous]
        public async Task<IEnumerable<TorrentViewModel>> Popular(int count)
        {
            var torrents = await _torrentService.PopularAsync(count);

            return _mapper.Map<IEnumerable<TorrentViewModel>>(torrents);
        }

        /// <summary>
        ///     Get information about the item.
        /// </summary>
        /// <param name="id">ID of the element.</param>
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<TorrentDetailsViewModel> Get(int id)
        {
            var torrent = await _torrentService.FindAsync(id);

            return _mapper.Map<TorrentDetailsViewModel>(torrent);
        }

        [HttpPost]
        public async Task<TorrentDetailsViewModel> Create(TorrentCreateViewModel model)
        {
            var torrent = _mapper.Map<Torrent>(model);

            torrent.UserId = User.GetUserId();

            torrent = await _torrentService.AddAsync(torrent);

            return _mapper.Map<TorrentDetailsViewModel>(torrent);
        }

        [HttpPut("{id}")]
        public async Task<TorrentDetailsViewModel> Update(int id, TorrentUpdateViewModel model)
        {
            var torrent = _mapper.Map<Torrent>(model);

            torrent = await _torrentService.UpdateAsync(id, User.GetUserId(), torrent);

            return _mapper.Map<TorrentDetailsViewModel>(torrent);
        }

        [HttpDelete("{id}"), Authorize(Policy = Policies.IsAdmin)]
        public async Task Delete(int id)
        {
            await _torrentService.DeleteAsync(id);
        }
    }
}