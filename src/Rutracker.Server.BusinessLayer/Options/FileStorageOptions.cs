namespace Rutracker.Server.BusinessLayer.Options
{
    public class FileStorageOptions
    {
        public string[] FileMimeTypes { get; set; }
        public string[] ImageMimeTypes { get; set; }
        public long FileMaxLength { get; set; }
        public long ImageMaxLength { get; set; }

        public string UserImageName { get; set; }
        public string TorrentImageName { get; set; }
        public string TorrentFileName { get; set; }
    }
}