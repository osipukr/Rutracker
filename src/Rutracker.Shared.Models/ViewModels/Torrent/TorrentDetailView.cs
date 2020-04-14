using System;
using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    [DataContract]
    public class TorrentDetailView : TorrentView
    {
        [DataMember(Order = 7)]
        public string Content { get; set; }

        [DataMember(Order = 10)]
        public DateTime? ModifiedDate { get; set; }
    }
}