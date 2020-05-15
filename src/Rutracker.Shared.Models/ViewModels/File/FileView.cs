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

        [DataMember(Order = 3)]
        public string Type { get; set; }

        [DataMember(Order = 4)]
        public string Url { get; set; }
    }
}