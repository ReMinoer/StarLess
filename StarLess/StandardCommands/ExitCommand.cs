using System;

namespace StarLess.StandardCommands
{
    internal class ExitCommand : Command
    {
        public event EventHandler ExitRequest;

        public ExitCommand(string applicationName)
            : base("exit", "Exit " + applicationName + ".")
        {
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            if (ExitRequest != null)
                ExitRequest(this, new EventArgs());
        }
    }
}