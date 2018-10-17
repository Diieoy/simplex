using System;

namespace BLLStandard.Exceptions
{
    public class NotUniqueNameException : Exception
    {
        public NotUniqueNameException()
        {
        }

        public NotUniqueNameException(string message)
            : base(message)
        {
        }

        public NotUniqueNameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
