using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels.Role
{
    [DataContract]
    public class RoleView : View<string>
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }
    }
}