namespace Rutracker.Client.BlazorWasm.Helpers
{
    public static class PageHelpers
    {
        public static bool IsValidImageUrl(string imageUrl)
        {
            return !string.IsNullOrWhiteSpace(imageUrl);
        }

        public static string TorrentLink(int id)
        {
            return string.Format(Constants.Path.Torrent, id.ToString());
        }

        public static string CategoryLink(int id)
        {
            return string.Format(Constants.Path.Category, id.ToString());
        }

        public static string UserLink(string userName)
        {
            return string.Format(Constants.Path.User, userName);
        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return bytes / 1024f / 1024f;
        }
    }
}