using System;

namespace Diese.ConsoleInterface
{
    public class ExitCommand : Command
    {
        public ExitCommand(string applicationName)
            : base("exit", "Exit " + applicationName + ".")
        { }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            Environment.Exit(0);
        }
    }
}