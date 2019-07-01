using System.Collections.Generic;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip,
                                                    int take,
                                                    string search,
                                                    IEnumerable<string> selectedTitleIds,
                                                    long? sizeFrom,
                                                    long? sizeTo)
            : base(search, selectedTitleIds, sizeFrom, sizeTo)
        {
            ApplyPaging(skip, take);
        }
    }
}