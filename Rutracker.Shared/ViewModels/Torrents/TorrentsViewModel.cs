﻿namespace Rutracker.Shared.ViewModels.Torrents
{
    public class TorrentsViewModel
    {
        public TorrentItemViewModel[] TorrentItems { get; set; }
        public PaginationInfoViewModel PageModel { get; set; }
        public SortViewModel SortModel { get; set; }
        public string SelectedTitle { get; set; }
    }
}  