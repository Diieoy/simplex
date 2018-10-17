using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class EventSeatDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int EventAreaId { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public int State { get; set; }
    }
}
