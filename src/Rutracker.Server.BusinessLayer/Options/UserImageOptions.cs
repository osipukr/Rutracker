namespace Rutracker.Server.BusinessLayer.Options
{
    public class UserImageOptions
    {
        public string[] MimeTypes { get; set; }
        public int MaxBytesLength { get; set; }
        public string FileName { get; set; }
    }
}