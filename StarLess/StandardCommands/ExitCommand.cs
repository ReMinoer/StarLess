using System;

namespace StarLess.StandardCommands
{
    public class ExitCommand : Command
    {
        public ExitCommand(string applicationName)
            : base("exit", "Exit " + applicationName + ".") {}

        public event EventHandler ExitRequest;

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            ExitRequest(this, new EventArgs());
        }
    }
}