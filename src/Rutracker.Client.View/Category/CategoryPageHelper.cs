namespace Rutracker.Client.View.Category
{
    public static class CategoryPageHelper
    {
        public const string CategoryMenuId = "Category-Action-Menu-Id";
        public const string CategoryShortMenuId = "Category-Short-Action-Menu-Id";
        public const string SubcategoryMenuId = "Subcategory-Action-Menu-Id";
        public const string SubcategoryShortMenuId = "Subcategory-Short-Action-Menu-Id";

        public static string GetCategoryCardId(int id) => $"category-card-{id}";
        public static string GetSubcategoryCardId(int id) => $"subcategory-card-{id}";
    }
}