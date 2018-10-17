using System;

namespace BLLStandard.Exceptions
{
    public class CanNotCreateEventSeatException : Exception
    {
        public CanNotCreateEventSeatException()
        {
        }

        public CanNotCreateEventSeatException(string message)
            : base(message)
        {
        }

        public CanNotCreateEventSeatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
