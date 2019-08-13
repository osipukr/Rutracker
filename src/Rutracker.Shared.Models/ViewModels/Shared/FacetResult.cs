namespace Rutracker.Shared.Models.ViewModels.Shared
{
    public class FacetResult<TEntity>
    {
        public FacetViewModel<TEntity>[] Items { get; set; }
    }
}