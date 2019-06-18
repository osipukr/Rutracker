namespace Rutracker.Client.Constants
{
    public static class ApiUris
    {
        public const string TorrentsIndex = "api/torrents/paging/?page={0}&pageSize={1}";
        public const string TorrentIndex = "api/torrents/?id={0}";
        public const string Titles = "api/torrents/titles/?count={0}";
    }
}