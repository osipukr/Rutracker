using System.Runtime.Serialization;

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
    }
}