using System.Runtime.Serialization;

namespace WcfServiceLibrary.CustomExceptions
{
    [DataContract]
    class CanNotDeleteEventSeatException
    {
        [DataMember]
        public string CustomError;

        public CanNotDeleteEventSeatException()
        {
        }

        public CanNotDeleteEventSeatException(string error)
        {
            CustomError = error;
        }
    }
}
