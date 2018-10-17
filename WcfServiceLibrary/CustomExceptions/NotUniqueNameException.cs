using System.Runtime.Serialization;

namespace WcfServiceLibrary.CustomExceptions
{
    [DataContract]
    class NotUniqueNameException
    {
        [DataMember]
        public string CustomError;

        public NotUniqueNameException()
        {
        }

        public NotUniqueNameException(string error)
        {
            CustomError = error;
        }
    }
}
