using System.Runtime.Serialization;

namespace WcfServiceLibrary.DTO
{
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        public decimal Account { get; set; }

        [DataMember]
        public string TimeZone { get; set; }

        [DataMember]
        public string Language { get; set; }
    }
}
