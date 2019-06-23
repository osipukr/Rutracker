using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search, IEnumerable<string> selectedTitleIds, long? sizeFrom, long? sizeTo)
            : base(x => string.IsNullOrWhiteSpace(search) || x.Title.Contains(search))
        {
            var titleIds = selectedTitleIds?.Select(long.Parse).ToArray();

            ApplyAndCriteria(x => titleIds == null || titleIds.Length == 0 || titleIds.Contains(x.ForumId));
            ApplyAndCriteria(x => !sizeFrom.HasValue || x.Size >= sizeFrom);
            ApplyAndCriteria(x => !sizeTo.HasValue || x.Size <= sizeTo);

            ApplyOrderBy(x => x.Date);
        }
    }
}