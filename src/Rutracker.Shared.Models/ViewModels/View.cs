using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels
{
    public abstract class View
    {
    }

    [DataContract]
    public abstract class View<T> : View
    {
        [DataMember(Order = 0)]
        public T Id { get; set; }
    }
}