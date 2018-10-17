using System;

namespace BLLStandard.Exceptions
{
    public class CanNotDeleteEventException : Exception
    {
        public CanNotDeleteEventException()
        {
        }

        public CanNotDeleteEventException(string message)
            : base(message)
        {
        }

        public CanNotDeleteEventException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
