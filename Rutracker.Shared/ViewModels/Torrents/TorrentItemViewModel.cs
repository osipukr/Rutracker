using System;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentItemViewModel
    {
        public long Id { get; }
        public DateTime Date { get; }
        public long Size { get; }
        public string Title { get; }

        public TorrentItemViewModel(long id, DateTime date, long size, string title)
        {
            Id = id;
            Date = date;
            Size = size;
            Title = title;
        }
    }
}