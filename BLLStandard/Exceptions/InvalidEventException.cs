using System;

namespace BLLStandard.Exceptions
{
    public class InvalidEventException : Exception
    {
        public InvalidEventException()
        {
        }

        public InvalidEventException(string message)
            : base(message)
        {
        }

        public InvalidEventException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
