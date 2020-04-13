using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentFilter : PagedFilter, ITorrentFilter
    {
        public int? CategoryId { get; set; }

        public int? SubcategoryId { get; set; }

        public string Search { get; set; }
    }
}