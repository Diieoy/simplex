using System;

namespace BLLStandard.Exceptions
{
    public class CanNotDeleteEventSeatException : Exception
    {
        public CanNotDeleteEventSeatException()
        {
        }

        public CanNotDeleteEventSeatException(string message)
            : base(message)
        {
        }

        public CanNotDeleteEventSeatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
