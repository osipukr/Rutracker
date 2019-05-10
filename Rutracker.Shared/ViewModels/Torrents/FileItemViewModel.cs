namespace Rutracker.Shared.ViewModels.Torrents
{
    public class FileItemViewModel
    {
        public long Size { get; set; }
        public string Name { get; set; }

        public FileItemViewModel()
        {
        }

        public FileItemViewModel(long size, string name)
        {
            Size = size;
            Name = name;
        }
    }
} 