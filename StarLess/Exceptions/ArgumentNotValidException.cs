namespace StarLess.Exceptions
{
    public class ArgumentNotValidException : ConsoleInterfaceException
    {
        public ArgumentNotValidException(Argument a, int idArg)
            : base(
                string.Format("Unvalid value for argument n°{0}.{1}", idArg,
                    ((a.GetUnvalidMessage() != "") ? " (" + a.GetUnvalidMessage() + ")" : ""))) {}
    }
}