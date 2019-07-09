namespace Rutracker.Client.Settings
{
    public static class ApiUriSettings
    {
        public const string TorrentsIndex = "api/torrents/pagination/?page={0}&pageSize={1}";
        public const string TorrentIndex = "api/torrents/?id={0}";
        public const string Titles = "api/torrents/titles/?count={0}";
    }
}