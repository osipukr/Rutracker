using System;
using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    [DataContract]
    public class SubcategoryView : View<int>
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Description { get; set; }

        [DataMember(Order = 3)]
        public DateTime AddedDate { get; set; }

        [DataMember(Order = 4)]
        public DateTime? ModifiedDate { get; set; }

        [DataMember(Order = 5)]
        public int TorrentsCount { get; set; }

        [DataMember(Order = 6)]
        public int CategoryId { get; set; }
    }
}