using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search, string[] titles, long? sizeFrom, long? sizeTo)
            : base(x => string.IsNullOrWhiteSpace(search) || EF.Functions.Like(x.Title, $"%{search}%"))
        {
            ApplyAndCriteria(x => titles == null || titles.Length == 0 || titles.Contains(x.ForumId.ToString()));
            ApplyAndCriteria(x => !sizeFrom.HasValue || x.Size >= sizeFrom);
            ApplyAndCriteria(x => !sizeTo.HasValue || x.Size <= sizeTo);

            ApplyOrderBy(x => x.Id);
        }
    }
}