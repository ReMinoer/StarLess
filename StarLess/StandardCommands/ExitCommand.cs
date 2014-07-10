using System;

namespace StarLess.StandardCommands
{
    internal class ExitCommand : Command
    {
        public ExitCommand(string applicationName)
            : base("exit", "Exit " + applicationName + ".") {}

        public event EventHandler ExitRequest;

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            ExitRequest(this, new EventArgs());
        }
    }
}