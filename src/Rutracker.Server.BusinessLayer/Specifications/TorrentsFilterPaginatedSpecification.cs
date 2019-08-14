using System.Collections.Generic;

namespace Rutracker.Server.BusinessLayer.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip, int take,
                                                    string search,
                                                    IEnumerable<string> selectedTitleIds,
                                                    long? sizeFrom, long? sizeTo)
            : base(search, selectedTitleIds, sizeFrom, sizeTo)
        {
            base.ApplyPaging(skip, take);
        }
    }
}