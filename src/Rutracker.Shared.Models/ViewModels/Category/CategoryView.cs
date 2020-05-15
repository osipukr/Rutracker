using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Shared.Models.ViewModels.Category
{
    [DataContract]
    public class CategoryView : View<int>
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Description { get; set; }

        [DataMember(Order = 3)]
        public int SubcategoriesCount { get; set; }

        [DataMember(Order = 4)]
        public DateTime AddedDate { get; set; }

        [DataMember(Order = 5)]
        public DateTime? ModifiedDate { get; set; }

        [DataMember(Order = 6)]
        public IEnumerable<SubcategoryView> Subcategories { get; set; }
    }
}