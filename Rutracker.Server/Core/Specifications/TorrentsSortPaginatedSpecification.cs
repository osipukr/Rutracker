using Microsoft.EntityFrameworkCore;

namespace Rutracker.Server.Core.Specifications
{
    public class TorrentsSortPaginatedSpecification : TorrentsFilterPaginatedSpecification
    {
        public TorrentsSortPaginatedSpecification(int skip, int take, string search, string property, string order)
            : base(skip, take, search)
        {
            try
            {
                if (order.Equals("Desc"))
                {
                    base.ApplyOrderByDescending(x => EF.Property<object>(x, property));
                }
                else
                {
                    base.ApplyOrderBy(x => EF.Property<object>(x, property));
                }
            }
            catch
            {
                base.ApplyOrderBy(x => x.Id);
            }
        }
    }
}