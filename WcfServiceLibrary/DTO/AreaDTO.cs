using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class AreaDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int LayoutId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int CoordX { get; set; }

        [DataMember]
        public int CoordY { get; set; }
    }
}
