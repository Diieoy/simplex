using System.Runtime.Serialization;

namespace WcfServiceLibrary.CustomExceptions
{
    [DataContract]
    class CanNotCreateEventSeatException
    {
        [DataMember]
        public string CustomError;

        public CanNotCreateEventSeatException()
        {
        }

        public CanNotCreateEventSeatException(string error)
        {
            CustomError = error;
        }
    }
}
