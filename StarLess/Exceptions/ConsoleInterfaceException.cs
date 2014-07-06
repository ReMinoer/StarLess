using System;

namespace StarLess.Exceptions
{
    internal abstract class ConsoleInterfaceException : Exception
    {
        protected ConsoleInterfaceException(string message)
            : base(message) {}
    }
}