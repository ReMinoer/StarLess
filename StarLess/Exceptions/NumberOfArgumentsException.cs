namespace StarLess.Exceptions
{
    public class NumberOfArgumentsException : ConsoleInterfaceException
    {
        public NumberOfArgumentsException(int given, int need)
            : base(
                string.Format("{0}. (given : {1}, need : {2})",
                    (given > need ? "Too many arguments" : "Not enough arguments"), given, need)) {}
    }
}