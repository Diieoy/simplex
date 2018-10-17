using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class EventInfoDTO
    {
        [DataMember]
        public string VenueName { get; set; }

        [DataMember]
        public string VenueDescription { get; set; }

        [DataMember]
        public string VenueAddress { get; set; }

        [DataMember]
        public string VenuePhone { get; set; }


        [DataMember]
        public string LayoutName { get; set; }

        [DataMember]
        public string LayoutDescription { get; set; }
    }
}
