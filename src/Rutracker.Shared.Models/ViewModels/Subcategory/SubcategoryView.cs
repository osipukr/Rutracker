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
        public int TorrentsCount { get; set; }
    }
}