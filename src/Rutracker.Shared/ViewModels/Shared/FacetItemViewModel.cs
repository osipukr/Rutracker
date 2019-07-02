namespace Rutracker.Shared.ViewModels.Shared
{
    public class FacetItemViewModel<TEntity>
    {
        public string Id { get; set; }
        public TEntity Value { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }
    }
}