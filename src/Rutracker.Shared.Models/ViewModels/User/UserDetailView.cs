using System.Collections.Generic;
using System.Runtime.Serialization;
using Rutracker.Shared.Models.ViewModels.Role;

namespace Rutracker.Shared.Models.ViewModels.User
{
    [DataContract]
    public class UserDetailView : UserView
    {
        [DataMember(Order = 5)]
        public string Email { get; set; }

        [DataMember(Order = 6)]
        public string PhoneNumber { get; set; }

        [DataMember(Order = 7)]
        public IEnumerable<RoleView> Roles { get; set; }
    }
}