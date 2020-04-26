using System;
using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    [DataContract]
    public class CategoryDetailView : CategoryView
    {
        [DataMember(Order = 5)]
        public DateTime AddedDate { get; set; }

        [DataMember(Order = 6)]
        public DateTime? ModifiedDate { get; set; }
    }
}