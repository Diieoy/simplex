using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class VenueDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Phone { get; set; }
    }
}
