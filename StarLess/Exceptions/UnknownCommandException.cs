namespace StarLess.Exceptions
{
    public class UnknownCommandException : ConsoleInterfaceException
    {
        public UnknownCommandException(string commandName)
            : base(string.Format("Unknown command : \"{0}\".", commandName)) {}
    }
}