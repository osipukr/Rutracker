using System;
using System.Runtime.Serialization;
using Rutracker.Shared.Models.ViewModels.Category;
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
        public string Content { get; set; }

        [DataMember(Order = 4)]
        public long Size { get; set; }

        [DataMember(Order = 5)]
        public string Hash { get; set; }

        [DataMember(Order = 6)]
        public int? TrackerId { get; set; }

        [DataMember(Order = 7)]
        public DateTime AddedDate { get; set; }

        [DataMember(Order = 8)]
        public DateTime? ModifiedDate { get; set; }

        [DataMember(Order = 9)]
        public CategoryView Category { get; set; }

        [DataMember(Order = 10)]
        public SubcategoryView Subcategory { get; set; }
    }
}