namespace Rutracker.Client.View.User
{
    public static class UsersPageHelper
    {
        public static bool IsValidAvatarUrl(string url)
        {
            return !string.IsNullOrWhiteSpace(url);
        }
    }
}