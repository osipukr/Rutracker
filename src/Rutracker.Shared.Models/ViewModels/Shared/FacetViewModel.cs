namespace Rutracker.Shared.Models.ViewModels.Shared
{
    public class FacetViewModel<TEntity>
    {
        public FacetItemViewModel<TEntity>[] FacetItems { get; set; }
    }
}