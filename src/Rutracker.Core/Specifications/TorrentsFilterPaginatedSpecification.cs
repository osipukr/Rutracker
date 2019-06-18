using System.Collections.Generic;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip,
                                                    int take,
                                                    string search,
                                                    IEnumerable<string> titles,
                                                    long? sizeFrom,
                                                    long? sizeTo)
            : base(search, titles, sizeFrom, sizeTo)
        {
            ApplyPaging(skip, take);
        }
    }
}