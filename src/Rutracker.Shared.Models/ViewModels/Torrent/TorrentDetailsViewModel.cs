namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    public class TorrentDetailsViewModel : TorrentViewModel
    {
        public string Hash { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public ForumViewModel Forum { get; set; }
        public FileViewModel[] Files { get; set; }
    }
}