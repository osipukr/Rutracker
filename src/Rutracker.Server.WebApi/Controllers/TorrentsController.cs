using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The Torrent API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="404">If the item is null.</response>
    public class TorrentsController : BaseApiController
    {
        private readonly ITorrentService _torrentService;
        private readonly IMapper _mapper;

        public TorrentsController(ITorrentService torrentService, IMapper mapper)
        {
            _torrentService = torrentService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get all items on the page with information for pagination.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="filter">Information to filter elements.</param>
        [HttpPost(nameof(Pagination))]
        public async Task<PaginationResult<TorrentViewModel>> Pagination(int page, int pageSize, FilterViewModel filter)
        {
            var torrents = await _torrentService.ListAsync(page, pageSize, filter.CategoryId, filter.SubcategoryId, filter.Search);
            var count = await _torrentService.CountAsync(filter.CategoryId, filter.SubcategoryId, filter.Search);

            return new PaginationResult<TorrentViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Items = _mapper.Map<TorrentViewModel[]>(torrents)
            };
        }

        [HttpGet(nameof(Popular))]
        public async Task<IEnumerable<TorrentViewModel>> Popular(int count)
        {
            var torrents = await _torrentService.PopularTorrentsAsync(count);

            return _mapper.Map<IEnumerable<TorrentViewModel>>(torrents);
        }

        /// <summary>
        ///     Get information about the item.
        /// </summary>
        /// <param name="id">ID of the element.</param>
        [HttpGet]
        public async Task<TorrentDetailsViewModel> Get(int id)
        {
            var torrent = await _torrentService.FindAsync(id);

            return _mapper.Map<TorrentDetailsViewModel>(torrent);
        }
    }
}