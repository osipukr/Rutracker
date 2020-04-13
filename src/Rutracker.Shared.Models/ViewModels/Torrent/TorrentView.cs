using System;
using System.Runtime.Serialization;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Shared.Models.ViewModels.Torrent
{
    [DataContract]
    public class TorrentView : View<int>
    {
        [DataMember(Order =  1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Description { get; set; }

        [DataMember(Order = 3)]
        public long Size { get; set; }

        [DataMember(Order = 4)]
        public string Hash { get; set; }

        [DataMember(Order = 5)]
        public int? TrackerId { get; set; }

        [DataMember(Order = 6)]
        public DateTime AddedDate { get; set; }

        [DataMember(Order = 8)]
        public int CommentsCount { get; set; }

        [DataMember(Order = 9)]
        public SubcategoryView Subcategory { get; set; }
    }
}