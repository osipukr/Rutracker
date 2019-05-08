namespace Rutracker.Shared.ViewModels
{
    public class SortViewModel
    {
        public SortPropertyStateViewModel IdSort => SortPropertyStateViewModel.Id;
        public SortPropertyStateViewModel DateSort => SortPropertyStateViewModel.Date;
        public SortPropertyStateViewModel SizeSort => SortPropertyStateViewModel.Size;
        public SortPropertyStateViewModel TitleSort => SortPropertyStateViewModel.Title;

        public SortPropertyStateViewModel CurrentProperty { get; }
        public SortOrderStateViewModel CurrentOrder { get; }
        public SortOrderStateViewModel NextOrder { get; }

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