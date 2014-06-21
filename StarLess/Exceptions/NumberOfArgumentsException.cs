namespace Diese.ConsoleInterface.Exceptions
{
    public class NumberOfArgumentsException : ConsoleInterfaceException
    {
        public NumberOfArgumentsException(int given, int need)
            : base((given > need ? "Too many arguments" : "Not enough arguments")
                    + ". (given : " + given + ", need : " + need + ")") { }
    }
}