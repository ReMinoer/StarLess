using System;
using System.Collections.Generic;
using System.Linq;
using StarLess.Exceptions;

namespace StarLess
{
    public abstract class ConsoleInterface
    {
        public string Name { get; set; }
        public string WelcomeMessage { get; set; }

        private Dictionary<string, Command> Commands { get; set; }

        private static string exitKeyword = "exit";
        private static string helpKeyword = "help";

        protected ConsoleInterface(string name)
        {
            Name = name;

            WelcomeMessage = "Welcome in " + name + " !";

            Commands = new Dictionary<string, Command>();
            Commands.Add(exitKeyword, new ExitCommand(name));
            Commands.Add(helpKeyword, new HelpCommand(Commands));
        }

        protected void AddCommand(Command c)
        {
            Commands.Add(c.Keyword, c);
        }

        public void Run()
        {
            Console.WriteLine(WelcomeMessage);
            while (true)
                WaitRequest();
        }

        private void WaitRequest()
        {
            Console.WriteLine();
            Console.Write(Name + ">");
            Request(new Request(Console.ReadLine()));
        }

        public void Request(string[] args)
        {
            Request(new Request(args));
        }

        private void Request(Request request)
        {
            try
            {
                if (request.Command == "")
                    return;

                if (!Commands.Keys.Contains(request.Command))
                    throw new UnknownCommandException(request.Command);

                Commands[request.Command].Run(request.Arguments);
            }
            catch (ConsoleInterfaceException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}