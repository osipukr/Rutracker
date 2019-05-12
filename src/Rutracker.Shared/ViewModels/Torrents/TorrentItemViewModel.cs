using System;
using System.ComponentModel;

namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentItemViewModel
    {
        [DisplayName("Id")]
        public long Id { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("Size")]
        public long Size { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }
    }
}