using System;

namespace StarLess.Exceptions
{
    public abstract class ConsoleInterfaceException : Exception
    {
        protected ConsoleInterfaceException(string message)
            : base(message) {}
    }
}