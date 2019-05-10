namespace Rutracker.Shared.ViewModels
{
    public class SortViewModel
    {
        public SortPropertyStateViewModel IdSort => SortPropertyStateViewModel.Id;
        public SortPropertyStateViewModel DateSort => SortPropertyStateViewModel.Date;
        public SortPropertyStateViewModel SizeSort => SortPropertyStateViewModel.Size;
        public SortPropertyStateViewModel TitleSort => SortPropertyStateViewModel.Title;

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