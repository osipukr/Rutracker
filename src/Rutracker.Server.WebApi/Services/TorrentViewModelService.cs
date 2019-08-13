using System;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrents;

namespace Rutracker.Server.WebApi.Services
{
    public partial class TorrentViewModelService
    {
        private async Task<TorrentsIndexViewModel> TorrentsIndexCallbackAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            Guard.Against.Null(filter, nameof(filter));

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

            return new TorrentsIndexViewModel
            {
                TorrentItems = _mapper.Map<TorrentItemViewModel[]>(torrentsSource),
                PaginationModel = new PaginationViewModel
                {
                    CurrentPage = page,
                    TotalItems = totalItemsCount,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize)
                }
            };
        }

        private async Task<TorrentIndexViewModel> TorrentIndexCallbackAsync(long id)
        {
            var torrentsSource = await _torrentService.FindAsync(id);

            return new TorrentIndexViewModel
            {
                TorrentDetailsItem = _mapper.Map<TorrentDetailsItemViewModel>(torrentsSource)
            };
        }

        private async Task<FacetViewModel<string>> TitleFacetCallbackAsync(int count)
        {
            var facets = await _torrentService.ForumsAsync(count);

            return new FacetViewModel<string>
            {
                FacetItems = facets.Select(x => new FacetItemViewModel<string>
                {
                    Id = x.Id.ToString(),
                    Value = x.Value,
                    Count = x.Count
                }).ToArray()
            };
        }
    }
}