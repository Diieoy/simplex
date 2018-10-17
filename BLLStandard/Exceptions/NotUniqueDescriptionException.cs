using System;

namespace BLLStandard.Exceptions
{
    public class NotUniqueDescriptionException : Exception
    {
        public NotUniqueDescriptionException()
        {
        }

        public NotUniqueDescriptionException(string message)
            : base(message)
        {
        }

        public NotUniqueDescriptionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
