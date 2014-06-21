namespace StarLess.Exceptions
{
    public class ArgumentNotValidException : ConsoleInterfaceException
    {
        public ArgumentNotValidException(Argument a, int idArg)
            : base("Unvalid value for argument n°" + idArg + "."
                    + ((a.getUnvalidMessage() != "") ? " (" + a.getUnvalidMessage() + ")" : "")) { }
    }
}