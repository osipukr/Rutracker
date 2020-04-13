using System.Runtime.Serialization;

namespace Rutracker.Shared.Models.ViewModels.File
{
    [DataContract]
    public class FileView : View<int>
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public long Size { get; set; }
    }
}