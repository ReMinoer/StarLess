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
        public Dictionary<string, ICommand> Commands { get; set; }

        private bool _exit;
        private const string ExitKeyword = "exit";
        private const string HelpKeyword = "help";

        protected ConsoleInterface(string name)
        {
            Name = name;

            WelcomeMessage = "Welcome in " + name + " !";

            var exitCommand = new ExitCommand(Name);
            exitCommand.ExitRequest += OnExitRequest;

            Commands = new Dictionary<string, ICommand> {{ExitKeyword, exitCommand}};
            Commands.Add(HelpKeyword, new HelpCommand(Commands));
        }

        protected void AddCommand(ICommand c)
        {
            Commands.Add(c.Keyword, c);
        }

        public void Run()
        {
            Console.WriteLine(WelcomeMessage);
            Initialize();
            while (!_exit)
                WaitRequest();
        }

        protected abstract void Initialize();

        private void WaitRequest()
        {
            Console.WriteLine();
            Console.Write(Name + ">");
            Request(new Request(Console.ReadLine()));
        }

        public void Request(params string[] args)
        {
            Initialize();
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

        private void OnExitRequest(object sender, EventArgs args)
        {
            _exit = true;
        }
    }
}