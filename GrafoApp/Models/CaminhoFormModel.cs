using System.Runtime.Serialization;

namespace GrafoApp.Models
{
    [DataContract]
    public class CaminhoFormModel
    {
        [DataMember]
        public string VerticeA { get; set; }

        [DataMember]
        public string VerticeB { get; set; }

        [DataMember]
        public string TabId { get; set; }
    }
}