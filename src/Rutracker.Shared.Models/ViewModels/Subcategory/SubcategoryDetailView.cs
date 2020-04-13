using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels.Subcategory
{
    [DataContract]
    public class SubcategoryDetailView : SubcategoryView
    {
        [DataMember(Order = 4)]
        public DateTime AddedDate { get; set; }

        [DataMember(Order = 5)]
        public DateTime? ModifiedDate { get; set; }
    }
}