using System;
using System.Collections.Generic;

namespace StarLess.StandardCommands
{
    public class HelpCommand : Command
    {
        public Dictionary<string, ICommand> Commands { get; set; }

        public HelpCommand(Dictionary<string, ICommand> commands)
            : base("help", "Display a list of all the commands.")
        {
            Commands = commands;

            OptionalArguments.Add(new Argument("command", typeof(string), "name of a command", new Validator(x => Commands.ContainsKey(x), "Unknown command")));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            if (arguments.ContainsKey("command"))
            {
                Console.WriteLine();
                Console.Write(Commands[(string)arguments["command"]].CompleteDescription());
            }
            else
            {
                Console.WriteLine();
                foreach (var c in Commands.Values)
                {
                    Console.Write("\t" + c.Keyword + " : " + c.Description);
                    Console.WriteLine();
                }
            }
        }
    }
}