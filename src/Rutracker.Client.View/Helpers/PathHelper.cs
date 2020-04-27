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

        public string GetCategoryPath(int categoryId)
        {
            return string.Format(Constants.Page.Category, categoryId.ToString());
        }

        public string GetSubcategoryPath(int subcategoryId)
        {
            return string.Format(Constants.Page.Subcategory, subcategoryId.ToString());
        }
    }
}