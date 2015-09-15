﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StarLess.Exceptions;
using StarLess.StandardCommands;

namespace StarLess
{
    public abstract class ConsoleInterface
    {
        private const string ExitKeyword = "exit";
        private const string HelpKeyword = "help";

        private readonly Dictionary<string, ICommand> _commands;

        private bool _exit;
        protected string Name { get; set; }
        protected string WelcomeMessage { get; set; }

        protected ReadOnlyDictionary<string, ICommand> Commands
        {
            get { return new ReadOnlyDictionary<string, ICommand>(_commands); }
        }

        protected ConsoleInterface(string name)
        {
            Name = name;

            WelcomeMessage = "Welcome in " + name + " !";

            var exitCommand = new ExitCommand(Name);
            exitCommand.ExitRequest += OnExitRequest;

            _commands = new Dictionary<string, ICommand>
            {
                {ExitKeyword, exitCommand}
            };
            _commands.Add(HelpKeyword, new HelpCommand(_commands));
        }

        public void Run()
        {
            Console.WriteLine(WelcomeMessage);
            Initialize();
            while (!_exit)
                WaitRequest();
        }

        public void Request(params string[] args)
        {
            Initialize();
            Request(new Request(args));
        }

        protected void AddCommand(ICommand c)
        {
            _commands.Add(c.Keyword, c);
        }

        protected abstract void Initialize();

        private void WaitRequest()
        {
            Console.WriteLine();
            Console.Write(Name + ">");
            Request(new Request(Console.ReadLine()));
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