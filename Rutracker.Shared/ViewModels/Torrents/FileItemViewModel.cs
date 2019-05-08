namespace Rutracker.Shared.ViewModels.Torrents
{
    public class FileItemViewModel
    {
        public long Size { get; }
        public string Name { get; }

        public FileItemViewModel(long size, string name)
        {
            Size = size;
            Name = name;
        }
    }
} 