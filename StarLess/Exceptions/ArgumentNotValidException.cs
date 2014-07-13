using StarLess.Interfaces;

namespace StarLess.Exceptions
{
    internal class ArgumentNotValidException : ConsoleInterfaceException
    {
        public ArgumentNotValidException(IArgument a, int idArg)
            : base(
                string.Format("Unvalid value for argument n°{0}.{1}", idArg,
                    ((a.GetUnvalidMessage() != "") ? " (" + a.GetUnvalidMessage() + ")" : ""))) {}
    }
}