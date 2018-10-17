using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class EventDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStart { get; set; }

        [DataMember]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeFinish { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public int LayoutId { get; set; }
    }
}
