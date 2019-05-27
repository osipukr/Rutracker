namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip, int take, string search, string[] titles, long? sizeFrom, long? sizeTo)
            : base(search, titles, sizeFrom, sizeTo)
        {
            base.ApplyPaging(skip, take);
        }
    }
}