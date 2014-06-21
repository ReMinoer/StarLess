namespace Diese.ConsoleInterface.Exceptions
{
    public class UnknownCommandException : ConsoleInterfaceException
    {
        public UnknownCommandException(string commandName)
            : base("Unknown command : \"" + commandName + "\".") { }
    }
}