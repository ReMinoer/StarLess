using System;
using System.Collections.Generic;
using System.Linq;
using StarLess.Exceptions;
using StarLess.StandardCommands;

namespace StarLess
{
    public abstract class ConsoleInterface
    {
        public string Name { get; set; }
        public string WelcomeMessage { get; set; }

        private Dictionary<string, ICommand> Commands { get; set; }

        private static string exitKeyword = "exit";
        private static string helpKeyword = "help";

        protected ConsoleInterface(string name)
        {
            Name = name;

            WelcomeMessage = "Welcome in " + name + " !";

            Commands = new Dictionary<string, ICommand>();
            Commands.Add(exitKeyword, new ExitCommand(Name));
            Commands.Add(helpKeyword, new HelpCommand(Commands));
        }

        protected void AddCommand(ICommand c)
        {
            Commands.Add(c.Keyword, c);
        }

        public void Run()
        {
            Console.WriteLine(WelcomeMessage);
            Initialize();
            while (true)
                WaitRequest();
        }

        protected abstract void Initialize();

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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}