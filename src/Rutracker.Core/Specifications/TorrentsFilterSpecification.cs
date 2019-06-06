using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;
using System.Linq;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search, string[] titles, int? sizeFrom, int? sizeTo)
            : base(x => (string.IsNullOrWhiteSpace(search) || EF.Functions.Like(x.Title, $"%{search}%")) &&
                        (titles == null || titles.Length == 0 || titles.Contains(x.ForumId.ToString())) &&
                        (!sizeFrom.HasValue || sizeFrom < x.Size) &&
                        (!sizeTo.HasValue || sizeTo > x.Size))
        {
        }
    }
}