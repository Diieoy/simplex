using System.Runtime.Serialization;

namespace WcfServiceLibrary.CustomExceptions
{
    [DataContract]
    class InvalidEventException
    {
        [DataMember]
        public string CustomError;

        public InvalidEventException()
        {
        }

        public InvalidEventException(string error)
        {
            CustomError = error;
        }
    }
}
