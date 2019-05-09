namespace Rutracker.Server.Core.Specifications
{
    public class TorrentsFilterPaginatedSpecification : TorrentsFilterSpecification
    {
        public TorrentsFilterPaginatedSpecification(int skip, int take, string search)
            : base(search)
        {
            base.ApplyPaging(skip, take);
        }
    }
}