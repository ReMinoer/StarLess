using System;
using System.Collections.Generic;
using System.Linq;

namespace StarLess
{
    public class HelpCommand : Command
    {
        public Dictionary<string, Command> Commands { get; set; }

        public HelpCommand(Dictionary<string, Command> commands)
            : base("help", "Display a list of all the commands.")
        {
            Commands = commands;

            OptionalArguments.Add(new Argument("command", "name of a command", new Validator(x => Commands.ContainsKey(x), "Unknown command")));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            if (arguments.ContainsKey("command"))
                DisplayCompleteDescription(Commands[arguments["command"]]);
            else
                DisplaySummaries();
        }

        protected virtual void DisplaySummaries()
        {
            Console.WriteLine();
            foreach (Command c in Commands.Values)
            {
                Console.Write("\t" + c.Keyword + " : " + c.Description);
                Console.WriteLine();
            }
        }

        protected virtual void DisplayCompleteDescription(Command c)
        {
            Console.WriteLine();

            Console.Write(c.Keyword);

            foreach (Argument a in c.RequiredArguments)
                Console.Write(" " + a.Name);

            if (c.OptionalArguments.Any())
            {
                Console.Write(" (");
                foreach (Argument a in c.OptionalArguments)
                    Console.Write(" " + a.Name);
                Console.Write(" )");
            }

            if (c.Options.Any())
            {
                Console.Write(" -[");
                foreach (Option o in c.Options)
                    Console.Write(" " + o.ShortKey);
                Console.Write(" ]");
            }
            Console.WriteLine();

            Console.WriteLine("\nDESCRIPTION : ");
            Console.WriteLine("\t" + c.Description);

            if (c.RequiredArguments.Any())
                Console.WriteLine("\nARGUMENTS :");

            foreach (Argument a in c.RequiredArguments)
                Console.WriteLine("\t" + a.Name + " : " + a.Description);

            if (c.OptionalArguments.Any())
                Console.WriteLine("\nOPTIONAL ARGUMENTS :");

            foreach (Argument a in c.OptionalArguments)
                Console.WriteLine("\t" + a.Name + " : " + a.Description);

            if (c.Options.Any())
                Console.WriteLine("\nOPTIONS :");

            foreach (Option o in c.Options)
                Console.WriteLine("\t-" + o.ShortKey + "/--" + o.LongKey + " : " + o.Description);
        }
    }
}