using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels.User
{
    [DataContract]
    public class UserView : View<string>
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 4)]
        public string ImageUrl { get; set; }
    }
}