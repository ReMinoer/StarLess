namespace StarLess.Exceptions
{
    public abstract class ConsoleInterfaceException : System.Exception
    {
        protected ConsoleInterfaceException(string message)
            : base(message)
        {
        }
    }
}