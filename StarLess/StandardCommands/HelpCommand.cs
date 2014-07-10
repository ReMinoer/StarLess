using System;
using System.Collections.Generic;

namespace StarLess.StandardCommands
{
    internal class HelpCommand : Command
    {
        public Dictionary<string, ICommand> Commands { get; set; }

        public HelpCommand(Dictionary<string, ICommand> commands)
            : base("help", "Display a list of all the commands.")
        {
            Commands = commands;

            OptionalArguments.Add(new Argument("command", typeof(string), "name of a command",
                new Validator(x => Commands.ContainsKey(x), "Unknown command")));
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            if (args.ContainsKey("command"))
            {
                Console.WriteLine();
                Console.Write(Commands[args.Value<string>("command")].CompleteDescription());
            }
            else
            {
                Console.WriteLine();
                foreach (ICommand c in Commands.Values)
                {
                    Console.Write("\t" + c.Keyword + " : " + c.Description);
                    Console.WriteLine();
                }
            }
        }
    }
}