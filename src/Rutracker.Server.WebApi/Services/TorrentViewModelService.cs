using System;
using System.Linq;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Services
{
    public partial class TorrentViewModelService
    {
        private async Task<PaginationResult<TorrentViewModel>> TorrentsIndexCallbackAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var torrentsSource = await _torrentService.ListAsync(page, pageSize,
                filter.Search,
                filter.SelectedTitleIds,
                filter.SizeFrom,
                filter.SizeTo);

            var totalItemsCount = await _torrentService.CountAsync(
                filter.Search,
                filter.SelectedTitleIds,
                filter.SizeFrom,
                filter.SizeTo);

            return new PaginationResult<TorrentViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize),
                Items = _mapper.Map<TorrentViewModel[]>(torrentsSource)
            };
        }

        private async Task<TorrentDetailsViewModel> TorrentIndexCallbackAsync(long id)
        {
            var torrentsSource = await _torrentService.FindAsync(id);

            return _mapper.Map<TorrentDetailsViewModel>(torrentsSource);
        }

        private async Task<FacetResult<string>> TitleFacetCallbackAsync(int count)
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