using System.Runtime.Serialization;

namespace WcfServiceLibrary.CustomExceptions
{
    [DataContract]
    class NotUniqueDescriptionException
    {
        [DataMember]
        public string CustomError;

        public NotUniqueDescriptionException()
        {
        }

        public NotUniqueDescriptionException(string error)
        {
            CustomError = error;
        }
    }
}
