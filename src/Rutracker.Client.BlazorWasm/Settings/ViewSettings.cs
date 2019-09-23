namespace Rutracker.Client.BlazorWasm.Settings
{
    public class PageSettings
    {
        public string ServiceName { get; set; }
        public int UsersPerPageCount { get; set; }
        public int TorrentsPerPageCount { get; set; }
        public int PopularTorrentsCount { get; set; }
        public int CommentsPerPageCount { get; set; }
    }
}