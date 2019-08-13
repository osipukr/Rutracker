using System;
using System.Linq;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Services
{
    public partial class TorrentViewModelService
    {
        private async Task<PaginationResult<TorrentViewModel>> TorrentsCallbackAsync(int page, int pageSize, FilterViewModel filter)
        {
            var torrents = await _torrentService.ListAsync(page, pageSize, filter.Search, filter.SelectedForumIds, filter.SizeFrom, filter.SizeTo);
            var count = await _torrentService.CountAsync(filter.Search, filter.SelectedForumIds, filter.SizeFrom, filter.SizeTo);

            return new PaginationResult<TorrentViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Items = _mapper.Map<TorrentViewModel[]>(torrents)
            };
        }

        private async Task<TorrentDetailsViewModel> TorrentCallbackAsync(long id)
        {
            var torrent = await _torrentService.FindAsync(id);

            return _mapper.Map<TorrentDetailsViewModel>(torrent);
        }

        private async Task<FacetResult<string>> ForumFacetCallbackAsync(int count)
        {
            var facets = await _torrentService.ForumsAsync(count);

            return new FacetResult<string>
            {
                Items = facets.Select(x => new FacetViewModel<string>
                {
                    Id = x.Item1.ToString(),
                    Value = x.Item2,
                    Count = x.Item3
                }).ToArray()
            };
        }
    }
}