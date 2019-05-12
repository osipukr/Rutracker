namespace Rutracker.Shared.ViewModels
{
    public class SortViewModel
    {
        public string IdSort => SortPropertyStateViewModel.Id.ToString();
        public string DateSort => SortPropertyStateViewModel.Date.ToString();
        public string SizeSort => SortPropertyStateViewModel.Size.ToString();
        public string TitleSort => SortPropertyStateViewModel.Title.ToString();

        public SortPropertyStateViewModel CurrentProperty { get; set; }
        public SortOrderStateViewModel CurrentOrder { get; set; }
        public SortOrderStateViewModel NextOrder { get; set; }

        public SortViewModel()
        {
        }

        public SortViewModel(SortPropertyStateViewModel property, SortOrderStateViewModel order)
        {
            NextOrder = order == SortOrderStateViewModel.Asc
                ? SortOrderStateViewModel.Desc
                : SortOrderStateViewModel.Asc;

            CurrentOrder = order;
            CurrentProperty = property;
        }
    }
}