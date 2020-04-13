namespace Rutracker.Shared.Infrastructure.Entities
{
    public static class StockRoles
    {
        private const string USER_ROLE_NAME = "User";
        private const string ADMIN_ROLE_NAME = "Admin";

        public static string User => USER_ROLE_NAME;

        public static string Admin => ADMIN_ROLE_NAME;
    }
}