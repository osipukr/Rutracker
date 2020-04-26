using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels
{
    [DataContract]
    public abstract class View<T>
    {
        [DataMember(Order = 0)]
        public T Id { get; set; }
    }
}