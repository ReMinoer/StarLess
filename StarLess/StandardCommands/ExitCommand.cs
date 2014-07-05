using System;

namespace StarLess.StandardCommands
{
    public class ExitCommand : Command
    {
        public event EventHandler ExitRequest;

        public ExitCommand(string applicationName)
            : base("exit", "Exit " + applicationName + ".")
        {}

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            ExitRequest(this, new EventArgs());
        }
    }
}