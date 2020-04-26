namespace Rutracker.Client.View.Helpers
{
    public class PathHelper
    {
        public string GetPageTitle(string page)
        {
            if (string.IsNullOrWhiteSpace(page))
            {
                return Constants.BrandName;
            }

            return $"{Constants.BrandName} - {page}";
        }
    }
}