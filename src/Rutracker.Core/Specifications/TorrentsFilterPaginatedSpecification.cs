namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip, int take, string search)
            : base(search)
        {
            ApplyPaging(skip, take);
        }
    }
}