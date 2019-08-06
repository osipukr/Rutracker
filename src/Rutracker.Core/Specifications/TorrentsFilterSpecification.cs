using System.Collections.Generic;
using System.Linq;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search, IEnumerable<string> selectedTitleIds, long? sizeFrom,long? sizeTo)
            : base(x => string.IsNullOrWhiteSpace(search) || x.Title.Contains(search))
        {
            var titleIds = selectedTitleIds?.Select(x => new
            {
                Success = long.TryParse(x, out var value),
                Value = value
            })
            .Where(x => x.Success)
            .Select(x => x.Value)
            .ToArray();

            base.ApplyAndCriteria(x => titleIds == null || titleIds.Length == 0 || titleIds.Contains(x.ForumId));
            base.ApplyAndCriteria(x => !sizeFrom.HasValue || x.Size >= sizeFrom);
            base.ApplyAndCriteria(x => !sizeTo.HasValue || x.Size <= sizeTo);

            base.ApplyOrderBy(x => x.Date);
        }
    }
}