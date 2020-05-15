using BlazorInputFile;

namespace Rutracker.Client.View.File
{
    public class FileLoad
    {
        public IFileListEntry File { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsLoading { get; set; }
    }
}