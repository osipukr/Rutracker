namespace Rutracker.Shared.Models.ViewModels.Shared
{
    public class FacetViewModel<TEntity>
    {
        public string Id { get; set; }
        public TEntity Value { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }
    }
}