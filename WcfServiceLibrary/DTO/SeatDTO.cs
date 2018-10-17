using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class SeatDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int AreaId { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Number { get; set; }
    }
}
