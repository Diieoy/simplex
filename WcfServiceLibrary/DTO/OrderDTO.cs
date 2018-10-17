using System;
using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class OrderDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int SeatId { get; set; }

        [DataMember]
        public DateTime DateTimeOrder { get; set; }
    }
}
