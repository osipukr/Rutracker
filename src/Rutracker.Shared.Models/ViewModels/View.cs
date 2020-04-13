using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels
{
    [DataContract]
    public class View<T>
    {
        [DataMember(Order = 0)]
        public T Id { get; set; }
    }
}