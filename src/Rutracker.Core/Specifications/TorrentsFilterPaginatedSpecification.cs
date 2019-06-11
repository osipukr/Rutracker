using System.Collections.Generic;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip, int take, string search, IReadOnlyCollection<string> titles, int? sizeFrom, int? sizeTo)
            : base(search, titles, sizeFrom, sizeTo)
        {
            ApplyPaging(skip, take);
        }
    }
}