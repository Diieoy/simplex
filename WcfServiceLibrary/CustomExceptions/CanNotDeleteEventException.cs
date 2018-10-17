using System.Runtime.Serialization;

namespace WcfServiceLibrary.CustomExceptions
{
    [DataContract]
    class CanNotDeleteEventException
    {
        [DataMember]
        public string CustomError;

        public CanNotDeleteEventException()
        {
        }

        public CanNotDeleteEventException(string error)
        {
            CustomError = error;
        }
    }
}
